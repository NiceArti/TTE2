using System.Collections.Generic;

namespace MeteorStudio.Utils
{
    public static class AdvancedRay <TObject>
    {
        private static Dictionary<string, TObject> _buffer = new Dictionary<string, TObject>();

        public static bool Select(TObject material, string tag) 
        {
            _buffer[tag] = material;
            return true;
        }

        public static bool Diselect(string tag) 
        {
            _buffer[tag] = default(TObject);
            return false;
        }

        public static bool IsSelected(string tag) 
        {
            if (_buffer[tag] != null)
                return true;

            return false;
        }

        public static TObject GetByTag(string tag) 
        {
            return _buffer[tag] == null ? default(TObject) : _buffer[tag];
        }

    }
}
