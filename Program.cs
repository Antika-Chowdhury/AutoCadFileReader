using System;
using System.Collections.Generic; // Add reference to System.Collections.Generic for List<T>
using System.IO;
using Newtonsoft.Json; // Add reference to Newtonsoft.Json for JSON serialization
using netDxf;

namespace AutoCadFileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Specify the path to the DXF file
                string dxfFilePath = "F:\\Autocad\\test1.dxf";

                // Load the DXF file
                DxfDocument dxf = DxfDocument.Load(dxfFilePath);

                // Create a list to store JSON objects for each entity
                var jsonEntities = new List<object>();

                // Iterate through all blocks in the DXF file
                foreach (var block in dxf.Blocks)
                {
                    // Iterate through all entities in the block
                    foreach (var entity in block.Entities)
                    {
                        // Create a dictionary to store attributes of the entity
                        var entityAttributes = new Dictionary<string, object>();

                        // Add entity type and layer name to the dictionary
                        entityAttributes["EntityType"] = entity.Type;
                        entityAttributes["LayerName"] = entity.Layer.Name;

                        // Example: Add specific information based on entity type
                        if (entity is netDxf.Entities.Line line)
                        {
                            entityAttributes["StartPoint"] = line.StartPoint;
                            entityAttributes["EndPoint"] = line.EndPoint;
                        }
                        else if (entity is netDxf.Entities.Circle circle)
                        {
                            entityAttributes["Center"] = circle.Center;
                            entityAttributes["Radius"] = circle.Radius;
                        }
                        // Add more checks and processing for other entity types as needed

                        // Add the entity attributes to the list
                        jsonEntities.Add(entityAttributes);
                    }
                }

                // Serialize the list of JSON entities to a JSON string
                string jsonOutput = JsonConvert.SerializeObject(jsonEntities, Formatting.Indented);

                // Specify the path for the output JSON file
                string outputFilePath = "F:\\Autocad\\output.json";

                // Write the JSON string to the output file
                File.WriteAllText(outputFilePath, jsonOutput);

                Console.WriteLine("JSON data saved to: " + outputFilePath);
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
