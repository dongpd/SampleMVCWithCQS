namespace SampleMVCWithCQS2.Application.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediatR;
    using System.Runtime.Serialization;

    /// <summary>
    /// Note that it is recommended to implement immutable Commands
    /// </summary>

    [DataContract]
    public class SaveProductCommand : IRequest<bool>
    {

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public bool InStock { get; set; }
        
        public SaveProductCommand(int id, string name, string category, decimal price, string color, bool inStock)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            Color = color;
            InStock = inStock;
        }

        public SaveProductCommand()
        {
            
        }
        
    }
}