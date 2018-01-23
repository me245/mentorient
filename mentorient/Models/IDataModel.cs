using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mentorient.Models
{
    interface IDataModel<K> where K : IEquatable<K>, IComparable
    {
       int Id { get; set; }
    }
}
