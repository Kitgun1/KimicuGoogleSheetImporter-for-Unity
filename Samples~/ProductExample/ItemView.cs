using System.Collections.Generic;
using GoogleSheetImporter.Samples.ProductExample;
using Kimicu.ExcelImporter;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private Product _productPrefab;

    private readonly List<Product> _spawnedProducts = new List<Product>();

    public void UpdateValues()
    {
        foreach (Product product in _spawnedProducts) Destroy(product.gameObject);
        _spawnedProducts.Clear();

        TextAsset jsonFile = Resources.Load<TextAsset>("Tables/products1");
        Debug.Log($"{jsonFile.text}");
        var gameSettings = JsonUtility.FromJson<GameSettings>(jsonFile.text);
        foreach (var product in gameSettings.Products)
        {
            var productObject = Instantiate(_productPrefab, _content);
            _spawnedProducts.Add(productObject);
            productObject.Initialize(product.Id, product.Name, product.Price);
        }
    }
}
