using System.Threading.Tasks;

namespace Axetay.Provision
{
    public interface IThingManager
    {
        Task RegisterThing(string thingName, string thingType);
    }
}