using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using System.Runtime.Serialization;

/// <summary>
/// Note that it is recommended to implement immutable Commands
/// </summary>

[DataContract]
public class DeleteProductCommand : IRequest<bool>
{
    [DataMember]
    public int Id { get; set; }


    public DeleteProductCommand(int id)
    {
        Id = id;
    }

    public DeleteProductCommand()
    {

    }
}