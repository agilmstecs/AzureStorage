using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using storageDemop.Models;
using System.Configuration;

namespace storageDemop.Controllers
{
    public class HomeController : Controller
    {
        CloudStorageAccount storageAccount;
        CloudTableClient tableClient;
        CloudTable table;

        public HomeController()
        {
            storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("AzureStorageConnectionString-1"));
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(CloudConfigurationManager.GetSetting("tableName"));
        }

        // GET: Home
        public ActionResult Index()
        {
            TableQuery<ProductsFromTable> query = new TableQuery<ProductsFromTable>();
            List<ProductsFromTable> products = new List<ProductsFromTable>();
            TableContinuationToken token = null;

            do
            {
                TableQuerySegment<ProductsFromTable> resultSegment = table.ExecuteQuerySegmented(query, token);
                token = resultSegment.ContinuationToken;
                foreach (ProductsFromTable product in resultSegment.Results)
                {
                    products.Add(product);
                }
            } while (token != null);

            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartitionKey, RowKey, id, ProductModel, Description")] ProductsFromTable product)
        {
            if (ModelState.IsValid)
            {
                TableOperation insertOperation = TableOperation.Insert(product);
                table.Execute(insertOperation);
            }
            return RedirectToAction("Index");
        }

    }
}