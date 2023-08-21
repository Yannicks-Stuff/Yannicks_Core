using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

// ReSharper disable MemberCanBePrivate.Global

namespace Yannick.IO;

/// <summary>
/// Represents an INI file parser.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public sealed partial class INI : IEnumerable<INI.IEntry>
{
    private readonly string _filePath;

    private readonly Dictionary<Group, List<(IEntry, IEntry?)>> _values = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="INI"/> class and parses the specified INI file.
    /// </summary>
    /// <param name="filePath">The path to the INI file to be parsed.</param>
    /// <param name="readFile">Read File Content and parse</param>
    /// <exception cref="ArgumentNullException">Thrown when the provided file path is null or empty.</exception>
    /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
    /// <exception cref="InvalidOperationException">Thrown when a duplicate group or key is found in the INI file.</exception>
    public INI(string filePath, bool readFile = true)
    {
        _filePath = filePath;

        var currentGroup = new Group { Index = 0, Value = "" };

        if (!readFile)
            return;

        var info = new FileInfo(filePath);

        if (!info.Exists)
            throw new ArgumentException("The file is not exists");

        var lines = File.ReadAllLines(filePath);
        for (uint i = 0; i < lines.Length; i++)
        {
            var (a, b) = ParseLine(lines[i], i);

            if (a == null && b == null)
                continue;

            if (a is Group group)
            {
                if (_values.ContainsKey(group))
                {
                    throw new InvalidOperationException($"Duplicate group '{group.Value}' found at line {i}.");
                }

                currentGroup = group;
                _values[group] = new List<(IEntry, IEntry?)>();
            }
            else if (a is KeyValue kv)
            {
                if (!_values.TryGetValue(currentGroup, out var value))
                {
                    value = new List<(IEntry, IEntry?)>();
                    _values[currentGroup] = value;
                }

                var existingKv =
                    value.FirstOrDefault(tuple => tuple.Item1 is KeyValue existing && existing.Key == kv.Key);
                if (existingKv.Item1 != null)
                {
                    throw new InvalidOperationException(
                        $"Duplicate key '{kv.Key}' found in group '{currentGroup.Value}' at line {i}.");
                }

                value.Add((a, b));
            }
            else if (a == null && b != null)
            {
                if (!_values.TryGetValue(currentGroup, out var value))
                {
                    value = new List<(IEntry, IEntry?)>();
                    _values[currentGroup] = value;
                }

                value.Add((b, null));
            }
            else
            {
                Console.WriteLine($"Invalid line at index {i}: {lines[i]}");
            }
        }
    }

    private INI(string filePath, Dictionary<Group, List<(IEntry, IEntry?)>> values)
    {
        _values = values;
        _filePath = filePath;
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key and group.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <param name="group">The group of the value to get or set.</param>
    /// <param name="comment">An optional comment to be added to the key-value pair line.</param>
    /// <returns>The value associated with the specified key and group. If the specified key and group are not found, returns null.</returns>
    public string? this[string key, string group = "", string? comment = null]
    {
        get
        {
            var groupEntry = new Group { Index = 0, Value = group };

            if (!_values.TryGetValue(groupEntry, out var entries))
                return null;

            var entry = entries.FirstOrDefault(e => e.Item1 is KeyValue mkv && mkv.Key == key);

            if (entry.Item1 is KeyValue kv)
                return kv.Value;

            return null;
        }
        set
        {
            if (value == null)
            {
                Remove(group, key);
            }
            else
            {
                AddOrUpdate(key, value, group, comment);
            }
        }
    }

    /// <summary>
    /// Gets or sets the group associated with the specified group.
    /// </summary>
    /// <param name="group">The group to get or set.</param>
    /// <returns>The value associated with the specified group. If the specified group is not found, returns null.</returns>
    public string? this[string group]
    {
        get => _values.ContainsKey(new Group { Index = 0, Value = group }) ? group : null;
        set
        {
            if (value == null)
                Remove(group);
            else if (value.Length > 0)
                AddOrUpdate(value);
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection of entries.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<IEntry> GetEnumerator()
    {
        foreach (var group in _values)
        {
            yield return group.Key;
            foreach (var (entry, comment) in group.Value)
            {
                yield return entry;
                if (comment != null)
                    yield return comment;
            }
        }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection of entries.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Determines whether the INI file contains the specified key within the specified group.
    /// </summary>
    /// <param name="key">The key to locate in the INI file.</param>
    /// <param name="group">The group in which to search for the key. If not specified, searches in the default group.</param>
    /// <returns>true if the INI file contains an element with the specified key within the specified group; otherwise, false.</returns>
    public bool Contains(string key, string group = "")
    {
        var groupObj = new Group { Index = 0, Value = group };

        return _values.TryGetValue(groupObj,
                   out var entries) &&
               entries.Any(entry => entry.Item1 is KeyValue kv && kv.Key == key);
    }

    /// <summary>
    /// Determines whether the INI file contains the specified group.
    /// </summary>
    /// <param name="group">The group to locate in the INI file.</param>
    /// <returns>true if the INI file contains an element with the specified group; otherwise, false.</returns>
    public bool ContainsGroup(string group) => _values.ContainsKey(new Group { Index = 0, Value = group });

    /// <summary>
    /// Adds or updates a group in the INI file.
    /// </summary>
    /// <param name="group">The name of the group to add or update.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="group"/> parameter is null or empty.</exception>
    public void AddOrUpdate(string group)
    {
        if (group.Length == 0)
            throw new ArgumentNullException(nameof(group), "You cannot delete the default group");

        var groupEntry = new Group { Index = (uint)_values.Count + 1, Value = group };

        if (_values.TryGetValue(groupEntry, out var val))
        {
            _values.Remove(groupEntry);
            _values.Add(groupEntry, val);
        }
        else
        {
            _values[groupEntry] = new List<(IEntry, IEntry?)>();
        }
    }

    /// <summary>
    /// Adds or updates a key-value pair within a specified group in the INI file.
    /// </summary>
    /// <param name="key">The key of the key-value pair.</param>
    /// <param name="value">The value of the key-value pair.</param>
    /// <param name="group">The group under which the key-value pair should be added or updated.</param>
    /// <param name="comment">An optional comment to be added to the key-value pair line.</param>
    public void AddOrUpdate(string key, string value, string group = "", string? comment = null)
    {
        if (key.Length == 0)
            throw new ArgumentException("Key cannot be empty.", nameof(key));

        var groupEntry = new Group { Index = 0, Value = group };
        if (!_values.TryGetValue(groupEntry, out var v))
        {
            v = new List<(IEntry, IEntry?)>();
            _values[groupEntry] = v;
        }

        var keyValueEntry = new KeyValue { Index = 0, Key = key, Value = value };

        Comment? commentEntry = null;
        if (!string.IsNullOrEmpty(comment))
            commentEntry = new Comment { Index = 0, Value = comment };

        var entryList = _values[groupEntry];
        var existingEntry = entryList.FirstOrDefault(e => e.Item1 is KeyValue kv && kv.Key == key);
        if (existingEntry.Item1 != null)
        {
            var index = entryList.IndexOf(existingEntry);
            entryList[index] = (keyValueEntry, commentEntry);
        }
        else
            entryList.Add((keyValueEntry, commentEntry));
    }

    /// <summary>
    /// Removes all entries associated with the specified group.
    /// </summary>
    /// <param name="group">The name of the group to remove.</param>
    public void Remove(string group)
    {
        if (group.Length == 0)
            throw new ArgumentException("Group name cannot be empty.", nameof(group));

        var groupEntry = new Group { Index = 0, Value = group };
        _values.Remove(groupEntry);
    }

    /// <summary>
    /// Removes a specific key-value pair from the specified group.
    /// </summary>
    /// <param name="group">The name of the group containing the key to remove.</param>
    /// <param name="key">The key to remove from the group.</param>
    public void Remove(string group, string key)
    {
        if (group.Length == 0)
            throw new ArgumentException("Group name cannot be empty.", nameof(group));

        if (key.Length == 0)
            throw new ArgumentException("Key name cannot be empty.", nameof(key));

        var groupEntry = new Group { Index = 0, Value = group };

        if (!_values.TryGetValue(groupEntry, out var entries))
            return;

        var index = entries.FindIndex(entry => entry.Item1 is KeyValue kv && kv.Key == key);
        if (index >= 0)
            entries.RemoveAt(index);
    }

    /// <summary>
    /// Saves the INI data to the file specified in the constructor.
    /// </summary>
    /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when access to the file is denied.</exception>
    public void Save()
    {
        var sb = new StringBuilder();

        foreach (var (group, entries) in _values)
        {
            if (!string.IsNullOrEmpty(group.Value))
            {
                sb.AppendLine($"[{group.Value}]");
            }

            foreach (var (entry, comment) in entries)
            {
                if (entry is KeyValue keyValue)
                {
                    sb.AppendLine($"{keyValue.Key}={keyValue.Value}");
                }

                if (comment != null)
                {
                    sb.AppendLine($"#{comment.Value}");
                }
            }
        }

        File.WriteAllText(_filePath, sb.ToString());
    }

    #region Lang

    public interface IEntry
    {
        public string Value { get; }
        public uint Index { get; }
    }

    public readonly struct Comment : IEntry
    {
        public string Value { get; init; }
        public uint Index { get; init; }
    }

    public readonly struct KeyValue : IEntry
    {
        public string Key { get; init; }
        public string Value { get; init; }
        public uint Index { get; init; }
    }

    public readonly struct Group : IEntry, IEquatable<Group>
    {
        public string Value { get; init; }
        public uint Index { get; init; }

        public bool Equals(Group other) => Value == other.Value;
        public override bool Equals(object? obj) => obj is Group other && Equals(other);
        public override int GetHashCode() => Value.GetHashCode();
    }

    #endregion

    #region STATIC

    [GeneratedRegex(@"#(.*)")]
    private static partial Regex _commentRegex();

    private static Regex CommentRegex => _commentRegex();

    [GeneratedRegex(@"\[(.*?)\]")]
    private static partial Regex _groupRegex();

    private static Regex GroupRegex => _groupRegex();

    [GeneratedRegex(@"(.*?)=(.*)")]
    private static partial Regex _keyValueRegex();

    private static Regex KeyValueRegex => _keyValueRegex();

    /// <summary>
    /// Parses a line of text and returns two <see cref="IEntry"/> objects representing
    /// the parsed content of the line.
    /// </summary>
    /// <param name="line">The line of text to parse.</param>
    /// <param name="index">The index of the line being parsed.</param>
    /// <returns>
    /// A tuple containing two <see cref="IEntry"/> objects. The first item in the tuple
    /// represents a <see cref="Group"/> or <see cref="KeyValue"/> object, depending on the
    /// content of the line. The second item represents a <see cref="Comment"/> object if
    /// the line contains a comment. If the line does not contain a comment, the second item
    /// will be <c>null</c>.
    /// </returns>
    /// <example>
    /// <code>
    /// var result = ParseLine("[Group] # A comment", 1);
    /// var group = result.Item1 as Group;
    /// var comment = result.Item2 as Comment;
    /// Console.WriteLine($"Group: {group?.Value}, Comment: {comment?.Value}");
    /// </code>
    /// </example>
    private static (IEntry?, IEntry?) ParseLine(string line, uint index)
    {
        IEntry? a = null, b = null;

        line = line.Trim();

        if (line.Length <= 3)
            return (a, b);

        var commentMatch = CommentRegex.Match(line);
        if (commentMatch.Success)
        {
            b = new Comment { Index = index, Value = commentMatch.Groups[1].Value };
            line = line.Replace(b.Value, "").Trim();
        }

        var groupMatch = GroupRegex.Match(line);
        if (groupMatch.Success)
        {
            a = new Group { Index = index, Value = groupMatch.Groups[1].Value };
            return (a, b);
        }

        var keyValueMatch = KeyValueRegex.Match(line);
        if (keyValueMatch.Success)
        {
            a = new KeyValue
                { Index = index, Key = keyValueMatch.Groups[1].Value, Value = keyValueMatch.Groups[2].Value };
        }

        return (a, b);
    }

    /// <summary>
    /// Fixes an INI file by analyzing and correcting its contents based on the provided parameters.
    /// </summary>
    /// <param name="filePath">The path to the INI file.</param>
    /// <param name="mergeGroups">If true, merges duplicate groups. If false, skips duplicate groups.</param>
    /// <param name="useLastKey">If true, uses the last key in case of duplicate keys. If false, uses the first key.</param>
    /// <param name="encoding">The character encoding to use when reading the INI file. Default is UTF-8.</param>
    /// <param name="removeInvalidEntries">If true, removes invalid entries from the INI file.</param>
    /// <returns>An INI object representing the corrected contents of the INI file.</returns>
    public static INI Fix(string filePath, bool mergeGroups = true, bool useLastKey = true,
        bool removeInvalidEntries = true, Encoding? encoding = null)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File '{filePath}' does not exist.");
        }

        var lines = File.ReadAllLines(filePath, encoding ?? Encoding.UTF8);
        var ini = new INI(filePath, false);
        var currentGroup = new Group { Index = 0, Value = "" };

        for (uint i = 0; i < lines.Length; i++)
        {
            try
            {
                var (a, b) = ParseLine(lines[i], i);
                switch (a)
                {
                    case null:
                        continue;
                    case Group group:
                    {
                        currentGroup = group;
                        if (!ini._values.ContainsKey(group) || mergeGroups)
                        {
                            ini._values[currentGroup] = new List<(IEntry, IEntry?)>();
                        }

                        break;
                    }
                    default:
                    {
                        if (!ini._values.TryGetValue(currentGroup, out var value))
                        {
                            value = new List<(IEntry, IEntry?)>();
                            ini._values[currentGroup] = value;
                        }

                        value.Add((a, b));
                        break;
                    }
                }
            }
            catch (InvalidDataException)
            {
                if (!removeInvalidEntries)
                {
                    throw;
                }
            }
        }

        var fixedValues = new Dictionary<Group, List<(IEntry, IEntry?)>>();
        foreach (var group in ini._values.Keys)
        {
            var entries = ini._values[group];
            var fixedEntries = new List<(IEntry, IEntry?)>();

            var seenKeys = new HashSet<string>();
            for (var i = entries.Count - 1; i >= 0; i--)
            {
                var entry = entries[i];
                if (entry.Item1 is KeyValue kv)
                {
                    if (seenKeys.Contains(kv.Key))
                    {
                        if (useLastKey)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        seenKeys.Add(kv.Key);
                    }
                }

                fixedEntries.Insert(0, entry);
            }

            fixedValues[group] = fixedEntries;
        }


        return new INI(filePath, fixedValues);
    }

    #endregion
}