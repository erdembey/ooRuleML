using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ooRuleML
{
    /*
     * ooRuleML C# Library
     *
     * @package    ooRuleML
     * @category   Library
     * @author     M. Erdem ÇORAPÇIOĞLU
     * @copyright  (c) 2006-2012
     * @license    LGPL v3
     */
    public class Equivalent : ICloneable
	{
        public Equivalent()
        {
            torso = new ArrayList();
        }

        public Equivalent(Equivalent another)
        {
            torso = new ArrayList();

            Oid refOid = null;
            torso = new ArrayList();

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Torso != null)
                {
                    Torso[] items = (Torso[])another.Torso.Clone();
                    torso.Clear();
                    foreach (Torso item in items)
                    {
                        torso.Add(new Torso(item));
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Equivalent other = new Equivalent((Equivalent)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (this.Torso.Length != other.Torso.Length)
            {
                return false;
            }

            for (int i = 0; i < Torso.Length; i++)
            {
                if (!Torso[i].Equals(other.Torso[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.oid != null)
            {
                code *= this.oid.GetHashCode();
            }

            if (this.Torso.Length > 0)
            {
                for (int i = 0; i < Torso.Length; i++)
                {
                    code *= Torso[i].GetHashCode();
                }
            }

            return code;
        }

        public void Reset()
        {
            index = -1;
        }

        [XmlIgnore]
        public object Current
        {
            get
            {
                if (index == 0)
                {
                    return oid;
                }
                else if (index == 1)
                {
                    return torso;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Next()
        {
            if (index < 2)
            {
                index++;
                return true;
            }
            return false;
        }

        public int AddTorso(Torso item)
        {
            return torso.Add(item);
        }

        [XmlElement(ElementName = "torso")]
        public Torso[] Torso
        {
            get
            {
                Torso[] items = new Torso[torso.Count];
                torso.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                Torso[] items = (Torso[])value;
                torso.Clear();
                foreach (Torso item in items)
                {
                    torso.Add(item);
                }
            }
        }

        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        public Object Clone()
        {
            return new Equivalent(this);
        }

        private Oid oid;
        private ArrayList torso;

        [XmlIgnore]
        private int index = -1;
	}
}
