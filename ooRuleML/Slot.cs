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
    public class Slot : ICloneable
    {
        public Slot()
        {
            this.individual = new ArrayList();
        }

        public Slot(Slot another)
        {
            if (another.Data != null)
            {
                data = another.Data;
            }

            individual = new ArrayList();
            if (another.Individual != null)
            {
                string[] items = (string[])another.Individual.Clone();
                individual.Clear();
                foreach (string item in items)
                {
                    individual.Add(new string(item.ToCharArray()));
                }
            }
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Slot other = new Slot((Slot)o);

            if (this.Data != null)
            {
                if (!this.Data.Equals(other.Data))
                {
                    return false;
                }
            }

            if (Individual.Length != other.Individual.Length)
            {
                return false;
            }

            for (int i = 0; i < Individual.Length; i++)
            {
                if (!Individual[i].Equals(other.Individual[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int code = 1;

            if (this.Data != null)
            {
                code *= this.Data.GetHashCode();
            }

            if (this.Individual.Length > 0)
            {
                for (int i = 0; i < Individual.Length; i++)
                {
                    code *= Individual[i].GetHashCode();
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
                    return data;
                }
                else if (index == 1)
                {
                    return individual;
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

        [XmlElement(ElementName = "Data")]
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        public int AddIndividual(string item)
        {
            return individual.Add(item);
        }

        [XmlElement(ElementName = "Ind")]
        public string[] Individual
        {
            get
            {
                string[] items = new string[individual.Count];
                individual.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                string[] items = (string[])value;
                individual.Clear();
                foreach (string item in items)
                {
                    individual.Add(item);
                }
            }
        }

        public Object Clone()
        {
            return new Slot(this);
        }

        private ArrayList individual;
        private string data;

        [XmlIgnore]
        private int index = -1;
    }
}
