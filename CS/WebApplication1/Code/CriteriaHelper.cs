using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Code {
    public static class CriteriaHelper {
        public static string ExtractDisplayText(CriteriaOperator criteria) {
            var visitor = new CustomVisitor();
            visitor.AcceptOperator(criteria);
            return string.Join(";", visitor.Values);
        }
        public static CriteriaOperator GetCriteriaByText(string text,string columnName, GroupOperatorType groupOperatorType) {
            var ids = text.Split(';');
            var operandProp = new OperandProperty(columnName);
            var criteriaList = Enumerable.Range(0, ids.Length).Select(s => new FunctionOperator(FunctionOperatorType.Contains, operandProp, ids[s]));           
            var criteria = new GroupOperator(groupOperatorType, criteriaList);
            return criteria;
        }
    }

    public class CustomVisitor : IClientCriteriaVisitor<CriteriaOperator> {
        public CustomVisitor() {
            Values = new List<string>();
        }
        public List<string> Values { get; private set; }
        public CriteriaOperator AcceptOperator(CriteriaOperator theOperator) {
            if (object.ReferenceEquals(theOperator, null))
                return null;
            return theOperator.Accept<CriteriaOperator>(this);
        }

        protected CriteriaOperatorCollection VisitOperands(CriteriaOperatorCollection operands) {
            foreach (CriteriaOperator operand in operands)
                AcceptOperator(operand);
            return operands;
        }
        protected virtual CriteriaOperator VisitGroup(GroupOperator theOperator) {
            VisitOperands(theOperator.Operands);
            return theOperator;
        }
        protected virtual CriteriaOperator VisitFunction(FunctionOperator theOperator) {
            VisitOperands(theOperator.Operands);
            return theOperator;
        }
        protected virtual CriteriaOperator VisitValue(OperandValue theOperand) {
            var constantValue = theOperand as ConstantValue;
            if (!ReferenceEquals(constantValue, null))
                Values.Add(constantValue.Value.ToString());
            return theOperand;
        }
        #region IClientCriteriaVisitor
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(JoinOperand theOperand) { throw new NotImplementedException(); }
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(OperandProperty theOperand) { return theOperand; }
        CriteriaOperator IClientCriteriaVisitor<CriteriaOperator>.Visit(AggregateOperand theOperand) { throw new NotImplementedException(); }
        #endregion
        #region ICriteriaVisitor
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(FunctionOperator theOperator) { return VisitFunction(theOperator); }
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(OperandValue theOperand) { return VisitValue(theOperand); }
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(GroupOperator theOperator) { return VisitGroup(theOperator); }
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(InOperator theOperator) { throw new NotImplementedException(); }
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(UnaryOperator theOperator) { throw new NotImplementedException(); }
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(BinaryOperator theOperator) { throw new NotImplementedException(); }
        CriteriaOperator ICriteriaVisitor<CriteriaOperator>.Visit(BetweenOperator theOperator) { throw new NotImplementedException(); }
        #endregion
    }
}
