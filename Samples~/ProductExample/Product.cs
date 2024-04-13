using UnityEngine;
using UnityEngine.UI;

namespace GoogleSheetImporter.Samples.ProductExample
{
    public class Product : MonoBehaviour
    {
       [SerializeField] private Text _id;
       [SerializeField] private Text _title;
       [SerializeField] private Text _price;

       public void Initialize(string id, string title, int price)
       {
           _id.text = $"Id: {id}";
           _title.text = $"Name: {title}";
           _price.text = $"Price: {price}";
       }
    }
}