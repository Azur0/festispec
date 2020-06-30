using System;

namespace FestiSpec.SharedCode.Repositories
{
    public interface ISoftDeletable
    {
        void Delete();

        DateTime? UpdatedAt { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}