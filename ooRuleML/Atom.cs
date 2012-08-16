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
    public class Atom : ICloneable, IComparable
    {
        public Atom()
        {
            this.individual = new ArrayList();
            this.variable = new ArrayList();
            this.slot = new ArrayList();
            this.data = new ArrayList();
            this.variableNames = new ArrayList();
            this.variableValues = new ArrayList();
        }

        public Atom(Atom another)
        {
            Oid refOid = null;
            individual = new ArrayList();
            variable = new ArrayList();
            slot = new ArrayList();
            data = new ArrayList();
            variableNames = new ArrayList();
            variableValues = new ArrayList();
            Degree refDegree = null;
            Operator refOperator = null;            

            try
            {
                if (another.Oid != null)
                {
                    refOid = (Oid)another.Oid.Clone();
                }

                if (another.Degree != null)
                {
                    refDegree = (Degree)another.Degree.Clone();
                }

                if (another.Operator != null)
                {
                    refOperator = (Operator)another.Operator.Clone();
                }

                if (another.Data != null)
                {
                    string[] items = (string[])another.Data.Clone();
                    foreach (string item in items)
                    {
                        data.Add(new string(item.ToCharArray()));
                    }
                }

                if (another.Individual != null)
                {
                    string[] items = (string[])another.Individual.Clone();
                    foreach (string item in items)
                    {
                        individual.Add(new string(item.ToCharArray()));
                    }
                }

                if (another.Variable != null)
                {
                    string[] items = (string[])another.Variable.Clone();
                    foreach (string item in items)
                    {
                        variable.Add(new string(item.ToCharArray()));
                    }
                }

                if (another.Slot != null)
                {
                    Slot[] items = (Slot[])another.Slot.Clone();
                    foreach (Slot item in items)
                    {
                        slot.Add(new Slot(item));
                    }
                }

                if (another.VariableNames != null)
                {
                    string[] items = (string[])another.VariableNames.Clone();
                    foreach (string item in items)
                    {
                        variableNames.Add(new string(item.ToCharArray()));
                    }
                }

                if (another.VariableValues != null)
                {
                    string[] items = (string[])another.VariableValues.Clone();
                    foreach (string item in items)
                    {
                        variableValues.Add(new string(item.ToCharArray()));
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

            oid = refOid;
            degree = refDegree;
            Operator = refOperator;
            fixedInd = another.FixedIndCount;
            assignedVariableCount = another.assignedVariableCount;
            variablesAssigned = another.variablesAssigned;
            index = -1;
        }

        public override bool Equals(object o)
        {
            if (o == null || GetType() != o.GetType())
            {
                return false;
            }

            Atom other = new Atom((Atom)o);

            if (this.oid != null)
            {
                if (!this.Oid.Equals(other.Oid))
                {
                    return false;
                }
            }

            if (this.Operator != null)
            {
                if (!this.Operator.Equals(other.Operator))
                {
                    return false;
                }
            }

            /*
             * if (this.Degree != null)
             * {
             *  if (!this.Degree.Equals(other.Degree))
             * {
             * return false;
             * }
             * }
             */

            if (this.Individual.Length != other.Individual.Length)
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

            if (this.Data.Length != other.Data.Length)
            {
                return false;
            }

            for (int i = 0; i < Data.Length; i++)
            {
                if (!Data[i].Equals(other.Data[i]))
                {
                    return false;
                }
            }

            if (this.Variable.Length != other.Variable.Length)
            {
                return false;
            }

            for (int i = 0; i < Variable.Length; i++)
            {
                if (!Variable[i].Equals(other.Variable[i]))
                {
                    return false;
                }
            }

            if (this.Slot.Length != other.Slot.Length)
            {
                return false;
            }

            for (int i = 0; i < Slot.Length; i++)
            {
                if (!Slot[i].Equals(other.Slot[i]))
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

            if (this.Operator != null)
            {
                code *= this.Operator.GetHashCode();
            }

            if (this.Degree != null)
            {
                code *= this.Degree.GetHashCode();
            }

            if (this.Individual.Length > 0)
            {
                for (int i = 0; i < Individual.Length; i++)
                {
                    code *= Individual[i].GetHashCode();
                }
            }

            if (this.Data.Length > 0)
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    code *= Data[i].GetHashCode();
                }
            }

            if (this.Variable.Length > 0)
            {
                for (int i = 0; i < Variable.Length; i++)
                {
                    code *= Variable[i].GetHashCode();
                }
            }

            if (this.Slot.Length > 0)
            {
                for (int i = 0; i < Slot.Length; i++)
                {
                    code *= Slot[i].GetHashCode();
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
                    return degree;
                }
                else if (index == 2)
                {
                    return individual;
                }
                else if (index == 3)
                {
                    return variable;
                }
                else if (index == 4)
                {
                    return opr;
                }
                else if (index == 5)
                {
                    return slot;
                }
                else if (index == 6)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public int CompareTo(object o)
        {
            Atom another = new Atom((Atom)o);

            if (another.Degree != null && degree != null)
            {
                double thiscf = double.Parse(degree.Data.ToString());
                double anothercf = double.Parse(another.Degree.Data.ToString());

                if (thiscf > anothercf)
                {
                    return -1;
                }
                else if (thiscf == anothercf)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else if (another.Degree != null)
            {
                return 1;
            }
            else if (degree != null)
            {
                return -1;
            }

            return 0;
        }

        public bool Next()
        {
            if (index < 7)
            {
                index++;
                return true;
            }
            return false;
        }

        public void assignVariableNames()
        {
            if (!variablesAssigned)
            {
                variableNames = new ArrayList();
                variableValues = new ArrayList();
                variablesAssigned = true;
                for (int i = 0; i < variable.Count; i++)
                {
                    variableNames.Add(variable[i]);
                    variableValues.Add("");
                }
            }
        }

        public bool variableExists(string var)
        {
            return variableNames.Contains(var);
        }

        [XmlIgnore]
        public string[] VariableNames
        {
            get
            {
                string[] items = new string[variableNames.Count];
                variableNames.CopyTo(items);
                return items;
            }
        }

        public void removeVariable(string var, int i)
        {
            variable.Remove(var);
        }

        public void setVariable(string var, int id)
        {
            variableValues[id] = var;
            assignedVariableCount++;
        }

        [XmlIgnore]
        public int AssignedVariableCount
        {
            get
            {
                return assignedVariableCount;
            }
        }

        [XmlIgnore]
        public string[] VariableValues
        {
            get
            {
                string[] items = new string[variableValues.Count];
                variableValues.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                string[] items = (string[])value;
                variableValues.Clear();
                foreach (string item in items)
                {
                    variableValues.Add(item);
                }
            }
        }

        [XmlElement(ElementName = "degree")]
        public Degree Degree
        {
            get { return degree; }
            set { degree = value; }
        }

        [XmlElement(ElementName = "op")]
        public Operator Operator
        {
            get { return opr; }
            set { opr = value; }
        }

        public int AddVariable(string item)
        {
            return variable.Add(item);
        }

        [XmlElement(ElementName = "Var")]
        public string[] Variable
        {
            get
            {
                string[] items = new string[variable.Count];
                variable.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                string[] items = (string[])value;
                variable.Clear();
                foreach (string item in items)
                {
                    variable.Add(item);
                }
            }
        }

        public int AddIndividual(string item)
        {
            return individual.Add(item);
        }

        public void ClearIndividual()
        {
            individual.Clear();
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

        public int AddData(string item)
        {
            return data.Add(item);
        }

        [XmlElement(ElementName = "Data")]
        public string[] Data
        {
            get
            {
                string[] items = new string[data.Count];
                data.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                string[] items = (string[])value;
                data.Clear();
                foreach (string item in items)
                {
                    data.Add(item);
                }
            }
        }

        public int AddSlot(Slot item)
        {
            return slot.Add(item);
        }

        [XmlElement(ElementName = "slot")]
        public Slot[] Slot
        {
            get
            {
                Slot[] items = new Slot[slot.Count];
                slot.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                Slot[] items = (Slot[])value;
                slot.Clear();
                foreach (Slot item in items)
                {
                    slot.Add(item);
                }
            }
        }

        [XmlElement(ElementName = "oid")]
        public Oid Oid
        {
            get { return oid; }
            set { oid = value; }
        }

        [XmlIgnore]
        public int FixedIndCount
        {
            get { return fixedInd; }
            set { fixedInd = value; }
        }

        public void initialIndividuals()
        {
            if (fixedInd == -1)
            {
                fixedInd = individual.Count;
            }
        }

        public Object Clone()
        {
            return new Atom(this);
        }

        private Degree degree;
        private Oid oid;
        private Operator opr;
        private ArrayList individual;
        private ArrayList variable;
        private ArrayList slot;
        private ArrayList data;

        [XmlIgnore]
        private int fixedInd=-1;
        [XmlIgnore]
        private int index = -1;
        [XmlIgnore]
        private ArrayList variableNames;
        [XmlIgnore]
        private ArrayList variableValues;
        [XmlIgnore]
        private bool variablesAssigned = false;
        [XmlIgnore]
        private int assignedVariableCount = 0;
    }
}