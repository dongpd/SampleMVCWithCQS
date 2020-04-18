namespace SampleMVCWithCQS.Application.Commands
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
    public class CreateProductCommand : IRequest<bool>
    {

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
        
        public CreateProductCommand(string name, string category, decimal price, string color, bool inStock)
        {
            Name = name;
            Category = category;
            Price = price;
            Color = color;
            InStock = inStock;
        }

        public CreateProductCommand()
        {
            
        }
        
    }
}