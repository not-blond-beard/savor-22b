using System;
using UnityEngine;
using UnityEngine.UI;

public class RefrigeratorUI : MonoBehaviour
{
    public Text stateId;
    public Text grade;
    public Text hp;
    public Text attack;
    public Text defense;
    public Text speed;

    public Text ingredientId;
    public Text recipeId;

    public Button FoodCreateButton;

    public void SetRefrigeratorData(Refrigerator refrigerator)
    {
        stateId.text = refrigerator.stateId.ToString();
        grade.text = refrigerator.grade;
        hp.text = refrigerator.hp.ToString();
        attack.text = refrigerator.attack.ToString();
        defense.text = refrigerator.defense.ToString();
        speed.text = refrigerator.speed.ToString();

        ingredientId.text = refrigerator.ingredientId.ToString();
        recipeId.text = refrigerator.recipeId.ToString();

        SetFoodCreateButton(refrigerator.stateId);
    }

    public void CreateFood(Guid stateId)
    {
        Debug.Log("Create food");
    }

    public void SetFoodCreateButton(Guid stateId)
    {
        FoodCreateButton.onClick.AddListener(() => CreateFood(stateId));
    }

}
