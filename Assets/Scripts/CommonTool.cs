public class CommonTool : ITool
{
    public float[] Multipliers;
    public float[] Additions;
    public PhysicalTrait AcceptedPhysical;

    public string Name { get; }
    public float[] ModifyAttributes(float[] attributes)
    {
        float[] result = new float[attributes.Length];

        

        return result;
    }

    public bool CanProcess(Ingredient ingredient)
    {
        return (ingredient.Physical & AcceptedPhysical) > 0;
    }

    public void Process(Ingredient ingredient)
    {
        for (int i = 0; i < ingredient.Attributes.Length; i++)
        {
            if (Multipliers[i] == 0)
            {
                ingredient.Attributes[i] = Additions[i];
                continue;
            }

            if (ingredient.Attributes[i] > 0)
            {
                ingredient.Attributes[i] = ingredient.Attributes[i] * Multipliers[i] + Additions[i];
            }
            else
            {
                ingredient.Attributes[i] = ingredient.Attributes[i] / Multipliers[i] + Additions[i];
            }
        }
    }
}