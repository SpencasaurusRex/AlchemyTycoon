public interface ITool
{
    string Name { get; }

    void Process(Ingredient ingredient);
    bool CanProcess(Ingredient ingredient);
}