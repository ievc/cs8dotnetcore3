using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packt.Shared;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.Serialization.Json;

namespace NorthwindFluent
{
    public class CategoriesViewModel
    {
        public class CategoryJson
        {
            public int categoryID;
            public string categoryName;
            public string desctiption;
        }

        public ObservableCollection<Category> Categories { get; set; }

        public CategoriesViewModel()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5001/");
                var serializer = new DataContractJsonSerializer(typeof(List<CategoryJson>));
                var stream = httpClient.GetStreamAsync("api/categories").Result;
                var cats = serializer.ReadObject(stream) as List<CategoryJson>;
                var categories = cats.Select(c =>
                    new Category()
                    {
                        CategoryID = c.categoryID,
                        CategoryName = c.categoryName,
                        Description = c.desctiption
                    });
                Categories = new ObservableCollection<Category>(categories);
            }
        }

    }
}
