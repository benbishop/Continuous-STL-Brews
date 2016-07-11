using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using StlBrews.Models;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.Xml.Linq;


namespace StlBrews.Services
{
    public class BreweryService
    {
        protected WebClient Client;
        public BreweryService()
        {
            Client = new WebClient();
             
        }    
        
        
        
        public List<Brewery> GetAll()
        {
            
            MemoryStream ms = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(GetBreweryJson()));
            
            
            var reader = JsonReaderWriterFactory.CreateJsonReader(ms, new XmlDictionaryReaderQuotas());
            
            XElement root = XElement.Load(reader);
            var els = root.Elements().ToList();
            var breweries = new List<Brewery>();
            foreach(var el in els){

                breweries.Add(
                new Brewery(){
                    name = GetJsonProp(el, "name")
                    }
                );
                
                
            } 
            return breweries;

        }
        
        public string GetBreweryJson()
        {
            return Client.DownloadString("http://rendr.io/brew_atlas/Brewery.json");
        }
        
        public string GetJsonProp(XElement el, string name)
        {
            return el.Elements().First(e => e.Name == name).Value;
            
        }
    }
    
    
}
