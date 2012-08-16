using System;
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
    public class ForAllFormula : ICloneable
    {
        public ForAllFormula()
        {
        }

        public ForAllFormula(ForAllFormula another)
        {
            Atom refAtom = null;
            ForAll refForall = null;
            Implies refImplies = null;
            Equivalent refEquivalent = null;

            try
            {
                if (another.Atom != null)
                {
                    refAtom = (Atom)another.Atom.Clone();
                }

                if (another.Equivalent != null)
                {
                    refEquivalent = (Equivalent)another.Equivalent.Clone();
                }

                if (another.Implies != null)
                {
                    refImplies = (Implies)another.Implies.Clone();
                }

                if (another.InnerForAll != null)
                {
                    refForall = (ForAll)another.InnerForAll.Clone();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            atom = refAtom;
            imp = refImplies;
            forall = refForall;
            eq = refEquivalent;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            ForAllFormula other = new ForAllFormula((ForAllFormula)o);

            if (this.Atom != null)
            {
                if (!this.Atom.Equals(other.Atom))
                {
                    return false;
                }
            }

            if (this.InnerForAll != null)
            {
                if (!this.InnerForAll.Equals(other.InnerForAll))
                {
                    return false;
                }
            }

            if (this.Implies != null)
            {
                if (!this.Implies.Equals(other.Implies))
                {
                    return false;
                }
            }

            if (this.Equivalent != null)
            {
                if (!this.Equivalent.Equals(other.Equivalent))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.atom != null)
            {
                code *= this.atom.GetHashCode();
            }

            if (this.forall != null)
            {
                code *= this.forall.GetHashCode();
            }

            if (this.imp != null)
            {
                code *= this.imp.GetHashCode();
            }

            if (this.eq != null)
            {
                code *= this.eq.GetHashCode();
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
                    return eq;
                }
                else if (index == 2)
                {
                    return imp;
                }
                else if (index == 3)
                {
                    return forall;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Next()
        {
            if (index < 4)
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

        [XmlElement(ElementName = "Forall")]
        public ForAll InnerForAll
        {
            get { return forall; }
            set { forall = value; }
        }

        [XmlElement(ElementName = "Implies")]
        public Implies Implies
        {
            get { return imp; }
            set { imp = value; }
        }

        [XmlElement(ElementName = "Equivalent")]
        public Equivalent Equivalent
        {
            get { return eq; }
            set { eq = value; }
        }

        public Object Clone()
        {
            return new ForAllFormula(this);
        }

        private Atom atom;
        private ForAll forall;
        private Implies imp;
        private Equivalent eq;

        [XmlIgnore]
        private int index = -1;
    }
}
