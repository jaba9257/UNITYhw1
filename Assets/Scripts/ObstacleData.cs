using UnityEngine;

[CreateAssetMenu(fileName = "NewObstacle", menuName = "Obstacle Data")] //расположение в меню добавления
public class ObstacleData : ScriptableObject
{
    public string obstacleName = "Obstacle"; //имя
    public Vector3 size = new Vector3(1, 1, 1); //размер куба
    public int damageAmount = 1; //урон
    public Color color = Color.red; //цвет
}