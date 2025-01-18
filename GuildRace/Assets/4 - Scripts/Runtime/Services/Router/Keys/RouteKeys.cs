/*
    Сгенерировано автоматически. Не редактировать!
*/
namespace AD.Services.Router
{
    [AD.ToolsCollection.KeyScript]
    public static class RouteKeys
    {
        public static class App
        {
            public static readonly RouteKey exit = new("a796446a14f24a88acb822d7c10ce5ac");
        }
        public static class Hub
        {
            public static readonly RouteKey main = new("d6f3669a9a564d27838e811fa8b61417");
            public static readonly RouteKey roster = new("242d02a360db413fbc203ce58955890c");
            public static readonly RouteKey recruiting = new("213fcd3913be4f9092dc700d8020ebfc");
            public static readonly RouteKey guildBank = new("3b396293764a4af5b1343cfbad262c57");
            public static readonly RouteKey instances = new("4355e6f15c634a8f9c9acc17a2ded30f");
            public static readonly RouteKey activeInstances = new("2b0e5656d63c4b7b88009e2b3ce52dc5");
            public static readonly RouteKey vendors = new("11d1048181b44ab88943a094f1b8871e");
        }
        public static class Guild
        {
            public static readonly RouteKey createGuild = new("dfd1c6090cfa4dd1bc63bf89d33b4041");
            public static readonly RouteKey removeCharacter = new("7a9396a31d514a2aa9b366e4a8f10f27");
            public static readonly RouteKey acceptJointRequest = new("5f5d0747d56b4d0f9aa1d75b73fee228");
            public static readonly RouteKey declineJointRequest = new("b610db7aafe4494e86002c1b7c38a10a");
            public static readonly RouteKey recruitingSettings = new("d99256e407a84e348d6cecdce00bcdb1");
        }
        public static class Inventory
        {
            public static readonly RouteKey splittingItemDialog = new("4bc6641ae77d42beb7f6b8bbd299c5ec");
            public static readonly RouteKey transferItemDialog = new("00bc352d72324586b868701491aa2207");
            public static readonly RouteKey discardItemDialog = new("06ac16fcc58645f098914571463282bc");
        }
        public static class Instances
        {
            public static readonly RouteKey setupInstance = new("ca322824650e4873b53fcfcdf970f543");
            public static readonly RouteKey currentInstance = new("920eb14bde51465bae20738f20777ca4");
        }
    }
}