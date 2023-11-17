using System.Text;
using Yannick.Extensions.ArrayExtensions.ByteArrayExtensions;
using Yannick.Extensions.StringExtensions;

namespace Yannick.Crypto.Compression;

public class Huffman : CompressionAndDeCompression
{
    public enum Code
    {
        FAST
    }

    public Huffman() : base(typeof(Code))
    {
    }

    public override int Encode(ReadOnlySpan<byte> source, Span<byte> target, int option)
    {
        throw new NotImplementedException();
    }

    public override int Decode(ReadOnlySpan<byte> source, Span<byte> target)
    {
        throw new NotImplementedException();
    }

    public override byte[]? Encode(byte[] source, int option)
    {
        var t = HuffmanCoding.BuildHuffmanTree(source.ToString(Encoding.UTF8));
        return HuffmanCoding.Encode(Encoding.UTF8.GetString(source), t).ToByteArray(Encoding.UTF8);
    }

    public override byte[]? Decode(byte[] source)
    {
        var t = HuffmanCoding.BuildHuffmanTree(source.ToString(Encoding.UTF8));
        return HuffmanCoding.Decode(source.ToString(Encoding.UTF8), t).ToByteArray(Encoding.UTF8);
    }

    public class HuffmanTreeNode
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public HuffmanTreeNode Left { get; set; }
        public HuffmanTreeNode Right { get; set; }
    }

    public class HuffmanTree
    {
        public HuffmanTreeNode Root { get; set; }

        public Dictionary<char, string> GetEncodingTable()
        {
            var encodingTable = new Dictionary<char, string>();
            BuildEncodingTable(Root, "", encodingTable);
            return encodingTable;
        }

        private void BuildEncodingTable(HuffmanTreeNode node, string prefix, Dictionary<char, string> encodingTable)
        {
            if (node == null)
            {
                return;
            }

            if (node.Character != '\0')
            {
                encodingTable.Add(node.Character, prefix);
            }
            else
            {
                BuildEncodingTable(node.Left, prefix + "0", encodingTable);
                BuildEncodingTable(node.Right, prefix + "1", encodingTable);
            }
        }
    }

    public static class HuffmanCoding
    {
        public static HuffmanTree BuildHuffmanTree(string input)
        {
            // Zähle die Häufigkeit jedes Zeichens im Text
            var frequencyTable = new Dictionary<char, int>();
            foreach (var c in input)
            {
                if (frequencyTable.ContainsKey(c))
                {
                    frequencyTable[c]++;
                }
                else
                {
                    frequencyTable.Add(c, 1);
                }
            }

            // Erstelle eine Liste von Blättern für jedes Zeichen im Text
            var leaves = new List<HuffmanTreeNode>();
            foreach (var entry in frequencyTable)
            {
                leaves.Add(new HuffmanTreeNode { Character = entry.Key, Frequency = entry.Value });
            } // Erstelle den Huffman-Baum, indem man die Blätter iterativ zu neuen Knoten kombiniert

            // Die Knoten werden anhand ihrer Häufigkeit sortiert
            while (leaves.Count > 1)
            {
                leaves = leaves.OrderBy(node => node.Frequency).ToList();

                var newNode = new HuffmanTreeNode
                {
                    Frequency = leaves[0].Frequency + leaves[1].Frequency,
                    Left = leaves[0],
                    Right = leaves[1]
                };

                leaves.RemoveAt(0);
                leaves.RemoveAt(0);
                leaves.Add(newNode);
            }

            // Der letzte verbleibende Knoten ist der Wurzelknoten des Huffman-Baums
            var huffmanTree = new HuffmanTree { Root = leaves.First() };
            return huffmanTree;
        }

        public static string Encode(string input, HuffmanTree huffmanTree)
        {
            var encodingTable = huffmanTree.GetEncodingTable();
            var encodedOutput = new List<string>();
            foreach (var c in input)
            {
                encodedOutput.Add(encodingTable[c]);
            }

            return string.Join("", encodedOutput);
        }

        public static string Decode(string input, HuffmanTree huffmanTree)
        {
            var decodedOutput = new List<char>();
            var currentNode = huffmanTree.Root;
            foreach (var c in input)
            {
                if (c == '0')
                {
                    currentNode = currentNode.Left;
                }
                else
                {
                    currentNode = currentNode.Right;
                }

                if (currentNode.Character != '\0')
                {
                    decodedOutput.Add(currentNode.Character);
                    currentNode = huffmanTree.Root;
                }
            }

            return new string(decodedOutput.ToArray());
        }
    }
}