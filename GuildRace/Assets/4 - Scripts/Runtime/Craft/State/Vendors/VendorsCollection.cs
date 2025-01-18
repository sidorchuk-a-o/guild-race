using AD.States;
using System.Collections.Generic;

namespace Game.Craft
{
    public class VendorsCollection : ReactiveCollectionInfo<VendorInfo>, IVendorsCollection
    {
        public VendorsCollection(IEnumerable<VendorInfo> values) : base(values)
        {
        }
    }
}