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
    public class Query : ICloneable
    {
        public Query()
        {
            formula = new ArrayList();
        }

        public Query(Query another)
        {
            formula = new ArrayList();
            Oid refOid = null;

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Formula != null)
                {
                    QueryFormula[] items = (QueryFormula[])another.Formula.Clone();
                    formula.Clear();
                    foreach (QueryFormula item in items)
                    {
                        formula.Add(new QueryFormula(item));
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

            Query other = new Query((Query)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (Formula.Length != other.Formula.Length)
            {
                return false;
            }

            for (int i = 0; i < Formula.Length; i++)
            {
                if (!Formula[i].Equals(other.Formula[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Oid != null)
            {
                code *= this.Oid.GetHashCode();
            }

            if (this.Formula.Length > 0)
            {
                for (int i = 0; i < Formula.Length; i++)
                {
                    code *= Formula[i].GetHashCode();
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
                    return formula;
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

        public int AddFormula(QueryFormula item)
        {
            return formula.Add(item);
        }

        [XmlElement(ElementName = "formula")]
        public QueryFormula[] Formula
        {
            get
            {
                QueryFormula[] items = new QueryFormula[formula.Count];
                formula.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                QueryFormula[] items = (QueryFormula[])value;
                formula.Clear();
                foreach (QueryFormula item in items)
                {
                    formula.Add(item);
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
            return new Query(this);
        }

        private Oid oid;
        private ArrayList formula;

        [XmlIgnore]
        private int index = -1;
    }
}
