using System;
using System.Drawing;

namespace Inventory.Core
{
    public record BalanceCompositeKey(int resourceid, int unitofmeasurementid);
    public record BalanceComposite(BalanceCompositeKey key, int receipted, int shipped);
}
