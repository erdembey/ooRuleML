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
    [XmlRoot("RuleML", Namespace = "http://www.ruleml.org/0.9/xsd" )]
    public class RuleML : ICloneable, IComparable
    {
        public RuleML()
        {
            parent = new ArrayList();
        }

        public RuleML(RuleML another)
        {
            Oid refOid = null;
            Assert refAssert = null;
            Query refQuery = null;
            Protect refProtect = null;
            parent = new ArrayList();
            try
            {
                if (another.Assert != null)
                {
                    refAssert = (Assert)another.Assert.Clone();
                }

                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Query != null)
                {
                    refQuery = (Query)another.Query.Clone();
                }

                if (another.Protect != null)
                {
                    refProtect = (Protect)another.Protect.Clone();
                }

                if (another.Parent != null)
                {
                    int[] items = (int[])another.Parent.Clone();
                    parent.Clear();
                    foreach (int item in items)
                    {
                        parent.Add(item);
                    }
                }
            }
            catch (Exception e) 
            {
                e.ToString();
            }

            assert = refAssert;
            oid = refOid;
            protect = refProtect;
            query = refQuery;
            id = another.id;
        }

        public int AddParent(int item)
        {
            return parent.Add(item);
        }

        public bool isParent(int item)
        {
            return parent.Contains(item);
        }

        [XmlIgnore]
        public int[] Parent
        {
            get
            {
                int[] items = new int[parent.Count];
                parent.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                int[] items = (int[])value;
                parent.Clear();
                foreach (int item in items)
                {
                    parent.Add(item);
                }
            }
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            RuleML other = new RuleML((RuleML)o);

            if (this.assert != null)
            {
                if (!this.Assert.Equals(other.Assert))
                {
                    return false;
                }
            }

            if (this.Protect != null)
            {
                if (!this.Protect.Equals(other.Protect))
                {
                    return false;
                }
            }

            if (this.Query != null)
            {
                if (!this.Query.Equals(other.Query))
                {
                    return false;
                }
            }

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.assert != null)
            {
                code *= this.assert.GetHashCode();
            }

            if (this.oid != null)
            {
                code *= this.oid.GetHashCode();
            }

            if (this.protect != null)
            {
                code *= this.protect.GetHashCode();
            }

            if (this.query != null)
            {
                code *= this.query.GetHashCode();
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
                    return assert;
                }
                else if (index == 1)
                {
                    return query;
                }
                else if (index == 2)
                {
                    return protect;
                }
                else
                {
                    return oid;
                }
            }
        }

        public bool Next()
        {
            if (index < 3)
            {
                index++;
                return true;
            }
            return false;
        }

        public int CompareTo(object o)
        {
            RuleML another = new RuleML((RuleML)o);

            if (another.assert != null && assert != null)
            {
                return assert.CompareTo(another.assert);
            }

            return 0;
        }

        [XmlElement(ElementName = "Assert")]
        public Assert Assert
        {
            get { return assert; }
            set { assert = value; }
        }

        [XmlElement(ElementName = "Query")]
        public Query Query
        {
            get { return query; }
            set { query = value; }
        }

        [XmlElement(ElementName = "Protect")]
        public Protect Protect
        {
            get { return protect; }
            set { protect = value; }
        }

        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        public bool isAssert()
        {
            if (this.assert != null)
            {
                return true;
            }
            return false;
        }

        public bool isQuery()
        {
            if (this.query != null)
            {
                return true;
            }
            return false;
        }

        public bool isProtect()
        {
            if (this.protect != null)
            {
                return true;
            }
            return false;
        }

        public Object Clone()
        {
            return new RuleML(this);
        }

        [XmlIgnore]
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        private Oid oid;
        private Assert assert;
        private Query query;
        private Protect protect;

        [XmlIgnore]
        private int id = 0;
        [XmlIgnore]
        private ArrayList parent;        
        [XmlIgnore]
        private int index = -1;
    }
}
