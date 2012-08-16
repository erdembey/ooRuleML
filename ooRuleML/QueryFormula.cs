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
     * @author     M. Erdem ÇORAPÇIOÐLU
     * @copyright  (c) 2006-2012
     * @license    LGPL v3
     */
    public class QueryFormula : ICloneable
    {
        public QueryFormula()
        {
            formula = new ArrayList();
        }

        public QueryFormula(QueryFormula another)
        {
            formula = new ArrayList();
            Atom refAtom = null;
            Exist refExist = null;

            try
            {
                if (another.Atom != null)
                {
                    refAtom = (Atom)another.Atom.Clone();
                }

                if (another.Exist != null)
                {
                    refExist = (Exist)another.Exist.Clone();
                }

                if (another.Formula != null)
                {
                    AndOrFormula[] items = (AndOrFormula[])another.Formula.Clone();
                    formula.Clear();
                    foreach (AndOrFormula item in items)
                    {
                        formula.Add(new AndOrFormula(item));
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            atom = refAtom;
            Exist = refExist;            
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            QueryFormula other = new QueryFormula((QueryFormula)o);

            if (this.Atom != null)
            {
                if (!this.Atom.Equals(other.Atom))
                {
                    return false;
                }
            }

            if (this.Exist != null)
            {
                if (!this.Exist.Equals(other.Exist))
                {
                    return false;
                }
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

            if (this.Atom != null)
            {
                code *= this.Atom.GetHashCode();
            }

            if (this.Exist != null)
            {
                code *= this.Exist.GetHashCode();
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
                    return atom;
                }
                else if (index == 1)
                {
                    return exist;
                }
                else if (index == 2)
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
            if (index < 3)
            {
                index++;
                return true;
            }
            return false;
        }

        [XmlElement(ElementName = "Atom")]
        public Atom Atom
        {
            get { return atom; }
            set { atom = value; }
        }

        [XmlElement(ElementName = "Exists")]
        public Exist Exist
        {
            get { return exist; }
            set { exist = value; }
        }

        public int AddFormula(AndOrFormula item)
        {
            return formula.Add(item);
        }

        [XmlElement(ElementName = "formula")]
        public AndOrFormula[] Formula
        {
            get
            {
                AndOrFormula[] items = new AndOrFormula[formula.Count];
                formula.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                AndOrFormula[] items = (AndOrFormula[])value;
                formula.Clear();
                foreach (AndOrFormula item in items)
                {
                    formula.Add(item);
                }
            }
        }

        public Object Clone()
        {
            return new QueryFormula(this);
        }

        private Atom atom;
        private Exist exist;
        private ArrayList formula;

        [XmlIgnore]
        private int index = -1;
    }
}
