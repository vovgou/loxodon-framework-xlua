#if XLUA
using UnityEngine;

namespace Loxodon.Framework.Views
{
    public enum ScriptReferenceType
    {
        TextAsset,
        Filename
    }

    [System.Serializable]
    public class ScriptReference : ISerializationCallbackReceiver
    {
        [SerializeField]
        protected TextAsset text;

        [SerializeField]
        protected string filename;

        [SerializeField]
        protected ScriptReferenceType type = ScriptReferenceType.TextAsset;

        public virtual ScriptReferenceType Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public virtual TextAsset Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public virtual string Filename
        {
            get { return this.filename; }
            set { this.filename = value; }
        }

        public void OnAfterDeserialize()
        {
#if !UNITY_EDITOR
            switch (type)
            {
                case ScriptReferenceType.TextAsset:
                    this.filename = null;
                    break;
                case ScriptReferenceType.Filename:
                    this.text = null;
                    break;
            }
#endif
        }

        public void OnBeforeSerialize()
        {
#if !UNITY_EDITOR
            switch (type)
            {
                case ScriptReferenceType.TextAsset:
                    this.filename = null;
                    break;
                case ScriptReferenceType.Filename:
                    this.text = null;
                    break;
            }
#endif
        }
    }
}
#endif