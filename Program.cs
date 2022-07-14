
Dictionary<string, HashSet<string>> keyValuePairs = new();
while (true)
{
    Console.Write(">");
    List<string> input = Console.ReadLine().Split(" ").ToList();
    CallFunc(input, (Operations)Enum.Parse(typeof(Operations), input[0]));
}

void CallFunc(List<string> input, Operations operation)
{
    switch (operation)
    {
        case Operations.ADD:
            {
                Add(input[1], input[2]);
                break;
            }
        case Operations.KEYS:
            {
                Keys();
                break;
            }
        case Operations.MEMBERS:
            {
                Members(input[1]);
                break;
            }
        case Operations.REMOVE:
            {
                Remove(input[1], input[2]);
                break;
            }
        case Operations.REMOVEALL:
            {
                RemoveAll(input[1]);
                break;
            }
        case Operations.CLEAR:
            {
                Clear();
                break;
            }
        case Operations.KEYEXISTS:
            {
                KeyExists(input[1]);
                break;
            }
        case Operations.MEMBEREXISTS:
            {
                MemberExists(input[1], input[2]);
                break;
            }
        case Operations.ALLMEMBERS:
            {
                AllMembers();
                break;
            }
        case Operations.ITEMS:
            {
                Items();
                break;
            }
        default: break;
    }
}

void Add(string key, string value)
{
    if (keyValuePairs.TryGetValue(key, out HashSet<string> values))
    {
        if (values.Contains(value))
        {
            PrintMessage(") ERROR, member already exists for key");
        }
        else
        {
            values.Add(value);
            PrintMessage(") Added");
        }
    }
    else
    {
        HashSet<string> vals = new HashSet<string>();
        vals.Add(value);
        keyValuePairs.Add(key, vals);
        PrintMessage(") Added");
    }
}

void Keys()
{
    PrintCollection(keyValuePairs.Keys);
}

void Members(string key)
{
    HashSet<string> values = new HashSet<string>();
    if (keyValuePairs.TryGetValue(key, out values))
    {
        PrintCollection(values);
    }
    else
    {
        PrintError(key);
    }
}

void Remove(string key, string value)
{
    HashSet<string> values = new HashSet<string>();
    if (keyValuePairs.TryGetValue(key, out values))
    {
        if (values.Remove(value))
        {
            PrintMessage("Removed");
            if (values.Count == 0)
            {
                keyValuePairs.Remove(key);
            }
        }
        else
        {
            PrintError("member");
        }
    }
    else
    {
        PrintError(key);
    }
}

void RemoveAll(string key)
{
    bool removed = keyValuePairs.Remove(key);
    if (removed)
    {
        PrintMessage(") Removed");
    }
    else
    {
        PrintError(key);
    }
}

void Clear()
{
    keyValuePairs.Clear();
    PrintMessage(") Cleared");
}

void KeyExists(string key)
{
    PrintMessage(") " + keyValuePairs.TryGetValue(key, out HashSet<string> values).ToString().ToLower());
}

void MemberExists(string key, string value)
{
    if (keyValuePairs.TryGetValue(key, out var values))
    {
        PrintMessage(values.TryGetValue(value, out string _).ToString());
    }
    else
    {
        PrintError(key);
    }
}

void AllMembers()
{
    ICollection<string> values = new List<string>();
    foreach (var key in keyValuePairs.Keys)
    {
        values = values.Concat(keyValuePairs[key]).ToList();
    }
    PrintCollection(values);
}

void Items()
{
    if (keyValuePairs.Keys.Count == 0)
    {
        PrintMessage(") empty set");
    }
    else
    {
        int i = 1;
        foreach (var key in keyValuePairs.Keys)
        {
            foreach (var value in keyValuePairs[key])
            {
                PrintMessage(i + ") " + key + ": " + value);
                i++;
            }
        }
    }
}

void PrintMessage(string message)
{
    Console.WriteLine(message);
}

void PrintError(string item)
{
    PrintMessage(") ERROR, " + item + " does not exist.");
}

void PrintCollection(ICollection<string> values)
{
    if (values.Count == 0)
    {
        PrintMessage(") empty set");
    }
    else
    {
        int i = 1;
        foreach (var item in values)
        {
            PrintMessage(i + ") " + item);
            i++;
        }
    }
}


public enum Operations
{
    KEYS,
    MEMBERS,
    ADD,
    REMOVE,
    REMOVEALL,
    CLEAR,
    KEYEXISTS,
    MEMBEREXISTS,
    ALLMEMBERS,
    ITEMS,
}