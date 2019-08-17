using System.Collections.Generic;

public static class ToolFactory
{
    static List<ITool> toolList;
    static Dictionary<string, ITool> toolLookup;

    static ToolFactory()
    {
        toolList = new List<ITool>();
        toolLookup = new Dictionary<string, ITool>();

        AddTool(new Alembic());
        AddTool(new Retort());
        AddTool(new Rack());
        AddTool(new Torch());
        AddTool(new Pot());
    }

    static void AddTool(ITool tool)
    {
        toolList.Add(tool);
        toolLookup.Add(tool.Name, tool);
    }

    public static ITool Get(string name)
    {
        return toolLookup[name];
    }

    public static ITool Get(int index)
    {
        return toolList[index];
    }
}

public class MortarAndPestle : ITool
{
    public string Name => "Mortar and Pestle";
    public void Process(Ingredient ingredient)
    {
        ingredient.Physical = PhysicalTrait.Powder;
    }

    public bool CanProcess(Ingredient ingredient)
    {
        return ingredient.Physical == PhysicalTrait.Solid;
    }
}

public class Alembic : CommonTool
{
    public string Name => "Alembic";

    public Alembic()
    {
        Multipliers = new[] { 1.25f, .8f, 1 };
        Additions   = new[] { .1f, -.2f, 0 };
        AcceptedPhysical = PhysicalTrait.Liquid;
    }
}

public class Retort : CommonTool
{
    public string Name => "Retort";

    public Retort()
    {
        Multipliers = new[] { 1f, 1f, 1f };
        Additions   = new[] { 0f, 0f, 0f };
    }
}

public class Rack : CommonTool
{
    public string Name => "Rack";

    public Rack()
    {
        Multipliers = new[] { 1f, 1f, 1f };
        Additions   = new[] { 0f, 0f, 0f };
    }
}

public class Torch : CommonTool
{
    public string Name => "Torch";

    public Torch()
    {
        Multipliers = new[] { 1f, 1f, 1f };
        Additions   = new[] { 0f, 0f, 0f };
    }
}

public class Pot : CommonTool
{
    public string Name => "Pot";

    public Pot()
    {
        Multipliers = new[] { 1f, 1f, 1f };
        Additions   = new[] { 0f, 0f, 0f };
    }
}