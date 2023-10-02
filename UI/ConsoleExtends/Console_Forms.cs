using System.Numerics;

namespace Yannick.UI;

public partial class Console
{
    public static class Forms
    {
        public abstract class Item : IEquatable<Item>
        {
            public Vector2 Position { get; set; } = new(0, 0);

            public Vector2 Dimension { get; set; } = new(WindowWidth, WindowHeight);
            public Item? Parent { get; set; } = null;
            public ConsoleColor Foreground { get; set; } = ForegroundColor;
            public ConsoleColor Background { get; set; } = BackgroundColor;
            public Vector2 VirtualPosition { get; protected set; } = new(0, 0);

            public bool Equals(Item? other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Position.Equals(other.Position) && VirtualPosition.Equals(other.VirtualPosition);
            }


            protected virtual void OnKeyPress(ConsoleKeyInfo keyInfo)
            {
            }

            protected Vector2 CalculateAbsolutePosition()
            {
                return new Vector2
                {
                    X = Position.X + VirtualPosition.X,
                    Y = Position.Y + VirtualPosition.Y
                };
            }

            public virtual void Draw() => Clear();

            public virtual void Clear()
            {
                var absolutePosition = CalculateAbsolutePosition();
                SetCursorPosition((int)absolutePosition.X, (int)absolutePosition.Y);

                for (var y = 0; y < Dimension.Y; y++)
                {
                    for (var x = 0; x < Dimension.X; x++)
                    {
                        Write(" ", Foreground, Background);
                    }

                    SetCursorPosition((int)absolutePosition.X, (int)absolutePosition.Y + y);
                }
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj.GetType() == this.GetType() && Equals((Item)obj);
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Position, VirtualPosition);
            }
        }

        public struct ItemChar
        {
            public ConsoleColor? Foreground { get; set; }
            public ConsoleColor? Background { get; set; }
            public char Char { get; set; }

            public static ItemChar[] Create(string txt, ConsoleColor? fb = null, ConsoleColor? bg = null)
            {
                var l = new List<ItemChar>();

                foreach (var t in txt.ToCharArray())
                    l.Add(new ItemChar
                    {
                        Background = bg,
                        Foreground = fb,
                        Char = t
                    });

                return l.ToArray();
            }
        }

        public class TextItem : Item
        {
            public ItemChar[] Text { get; set; }
            public uint VisibleTextLength { get; protected set; }


            public void Draw()
            {
                var absolutePosition = CalculateAbsolutePosition();
                SetCursorPosition((int)absolutePosition.X, (int)absolutePosition.Y);

                uint visibleChars = 0;
                var currentX = 0;
                var currentY = 0;

                foreach (var itemChar in Text)
                {
                    if (currentY >= Dimension.Y)
                        break;

                    switch (itemChar.Char)
                    {
                        case '\n':
                            currentY++;
                            currentX = 0;
                            SetCursorPosition((int)absolutePosition.X + currentX, (int)absolutePosition.Y + currentY);
                            break;
                        case '\t':
                            Write(new string(' ', 4), itemChar.Foreground ?? Foreground,
                                itemChar.Background ?? Background);
                            currentX += 4;
                            break;
                        default:
                            Write(itemChar.Char.ToString(), itemChar.Foreground ?? Foreground,
                                itemChar.Background ?? Background);
                            currentX++;
                            break;
                    }

                    if (currentX >= Dimension.X)
                    {
                        currentY++;
                        currentX = 0;
                        SetCursorPosition((int)absolutePosition.X + currentX, (int)absolutePosition.Y + currentY);
                    }

                    visibleChars++;
                }

                VisibleTextLength = visibleChars;
                VirtualPosition = new Vector2 { X = currentX, Y = currentY };
            }
        }

        public class Frame : Item
        {
        }

        public class Container : Item
        {
            protected List<Item> Children { get; } = new();
            public int Count => Children.Count;

            public Item? this[int index]
            {
                get => index > Children.Count || index < 0 ? null : Children[index];
                set
                {
                    if (value == null || index > Children.Count || index < 0)
                        return;

                    Children.Insert(index, value);
                }
            }

            public virtual void AddChild(Item child)
            {
                child.Parent = this;
                Children.Add(child);
            }

            public bool RemoveChild(Item child)
            {
                return Children.Remove(child);
            }

            public void InsertChildAt(int index, Item child)
            {
                child.Parent = this;
                Children.Insert(index, child);
            }

            public int IndexOfChild(Item child)
            {
                return Children.IndexOf(child);
            }

            public Item GetChildAt(int index)
            {
                return Children[index];
            }

            public override void Draw()
            {
                foreach (var child in Children)
                {
                    child.Draw();
                }
            }

            public override void Clear()
            {
                base.Clear();
                foreach (var child in Children)
                    child.Clear();
            }
        }

        public class FlexLayout : Container
        {
            public enum LayoutOrientation
            {
                Horizontal,
                Vertical
            }

            public enum SizeUnit
            {
                Absolute,
                Percentage
            }

            public LayoutOrientation Orientation { get; set; } = LayoutOrientation.Horizontal;
            private Dictionary<Item, FlexDimension> Widths { get; } = new Dictionary<Item, FlexDimension>();
            private Dictionary<Item, FlexDimension> Heights { get; } = new Dictionary<Item, FlexDimension>();

            public void AddChild(Item child, FlexDimension width, FlexDimension height)
            {
                base.AddChild(child);
                Widths[child] = width;
                Heights[child] = height;

                RecalculateDimensions();
            }

            public override void AddChild(Item child)
            {
                AddChild(child, new FlexDimension { Value = 1, Unit = SizeUnit.Percentage },
                    new FlexDimension { Value = 1, Unit = SizeUnit.Percentage });
            }

            private void RecalculateDimensions()
            {
                float totalAvailableWidth = Dimension.X;
                float totalPercentageWidth = 0;
                float totalAvailableHeight = Dimension.Y;
                float totalPercentageHeight = 0;

                // Zuerst berechnen wir die absolute Größe für alle Kinder
                foreach (var child in Children)
                {
                    if (Widths[child].Unit == SizeUnit.Absolute)
                        totalAvailableWidth -= Widths[child].Value;
                    else
                        totalPercentageWidth += Widths[child].Value;

                    if (Heights[child].Unit == SizeUnit.Absolute)
                        totalAvailableHeight -= Heights[child].Value;
                    else
                        totalPercentageHeight += Heights[child].Value;
                }

                // Wenn die Kinder in Summe breiter oder höher sind als der Container, dann begrenzen wir sie
                if (Orientation == LayoutOrientation.Horizontal && totalAvailableWidth < 0)
                {
                    float scale = Dimension.X / (Dimension.X - totalAvailableWidth);
                    foreach (var child in Children)
                    {
                        if (Widths[child].Unit == SizeUnit.Absolute)
                            Widths[child] = new FlexDimension
                                { Value = Widths[child].Value * scale, Unit = SizeUnit.Absolute };
                    }

                    totalAvailableWidth = 0;
                }
                else if (Orientation == LayoutOrientation.Vertical && totalAvailableHeight < 0)
                {
                    float scale = Dimension.Y / (Dimension.Y - totalAvailableHeight);
                    foreach (var child in Children)
                    {
                        if (Heights[child].Unit == SizeUnit.Absolute)
                            Heights[child] = new FlexDimension
                                { Value = Heights[child].Value * scale, Unit = SizeUnit.Absolute };
                    }

                    totalAvailableHeight = 0;
                }

                // Verteilen Sie den verbleibenden Raum an alle Kinder, die in Prozent festgelegt sind
                foreach (var child in Children)
                {
                    if (Orientation == LayoutOrientation.Horizontal)
                    {
                        child.Dimension = new Vector2
                        {
                            X = Widths[child].Unit == SizeUnit.Percentage
                                ? totalAvailableWidth * (Widths[child].Value / totalPercentageWidth)
                                : child.Dimension.X,
                            Y = Heights.ContainsKey(child) && Heights[child].Unit == SizeUnit.Absolute
                                ? Heights[child].Value
                                : Dimension.Y
                        };
                    }
                    else // LayoutOrientation.Vertical
                    {
                        child.Dimension = new Vector2
                        {
                            X = Widths.ContainsKey(child) && Widths[child].Unit == SizeUnit.Absolute
                                ? Widths[child].Value
                                : Dimension.X,
                            Y = Heights[child].Unit == SizeUnit.Percentage
                                ? totalAvailableHeight * (Heights[child].Value / totalPercentageHeight)
                                : child.Dimension.Y
                        };
                    }
                }
            }


            public override void Draw()
            {
                var currentX = Position.X;
                var currentY = Position.Y;

                foreach (var child in Children)
                {
                    child.Position = new Vector2 { X = currentX, Y = currentY };
                    child.Draw();

                    if (Orientation == LayoutOrientation.Horizontal)
                        currentX += child.Dimension.X;
                    else
                        currentY += child.Dimension.Y;
                }
            }

            public override void Clear()
            {
                base.Clear();

                foreach (var child in Children)
                {
                    child.Clear();
                }
            }

            public record FlexDimension
            {
                public float Value { get; init; }
                public SizeUnit Unit { get; init; } = SizeUnit.Absolute;
            }
        }
    }
}