//using System.Collections.Generic;

//public static class IngredientFactory
//{
//    static List<ITool> ingredientList;
//    static Dictionary<string, ITool> ingredientLookup;

//    static IngredientFactory()
//    {
//        ingredientList = new List<ITool>();
//        ingredientLookup = new Dictionary<string, ITool>();

//        AddIngredient(new Ingredient { Name="" });
//    }

//    static void AddIngredient(Ingredient ingredient)
//    {
//        ingredientList.Add(ingredient);
//        ingredientLookup.Add(ingredient.Name, ingredient);
//    }

//    public static ITool Get(string name)
//    {
//        return ingredientLookup[name];
//    }

//    public static ITool Get(int index)
//    {
//        return ingredientList[index];
//    }
//}