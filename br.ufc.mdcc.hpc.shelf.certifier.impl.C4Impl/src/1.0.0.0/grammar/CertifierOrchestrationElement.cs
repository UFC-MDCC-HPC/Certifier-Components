using System;
using System.Collections;
using System.Collections.Generic;

using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar{



public class CertifierOrchestrationElement {

	private XMLCertifierBase element; //Value of the node. This object is JAXB auto generated.
	private CertifierOrchestrationOperation operation; //operation related to the node
	private string operationStr;
	private List<CertifierOrchestrationElement> children; //list o children node
	private C4ImplVisitor saFeVisitor;

	public CertifierOrchestrationElement() {
		this.children = new List<CertifierOrchestrationElement>();
	}
	
	public void addWorflowElement(CertifierOrchestrationElement wf){
		this.children.Add(wf);
	}
	
	public XMLCertifierBase getElement() {
		return element;
	}

	public void setElement(XMLCertifierBase element) {
		this.element = element;
	}
	
	public CertifierOrchestrationOperation getOperation() {
		return operation;
	}

	public void setOperation(String oper) {
		this.operationStr = oper;
		oper = oper.ToUpper();
		//this.operation = CertifierOrchestrationOperation..valueOf(oper);
			this.operation = (CertifierOrchestrationOperation)Enum.Parse(typeof(CertifierOrchestrationOperation), oper);
		
		}
	
	public List<CertifierOrchestrationElement> getChildren() {
		return children;
	}
	


	public void accept(C4ImplVisitor visitor){
		this.saFeVisitor = visitor;
	}
	
	public int run(){ 
		int ret=CertifierConstants.NORMAL_SIGNAL;
		if(this.saFeVisitor!=null)
			ret= this.saFeVisitor.visit(this);
		//System.out.println("cert orch " + ret);
		return ret;
		
	}
	
	public String getOperationStr(){
		return this.operationStr;
	}
	

	public String toString() {
		String res="";
		res+=
				"["+(element).level+"]"
				+"["+(element).order+"]"
				+(element).oper_name;
		if(this.element.value!=null){
			res+="[value="+this.element.value+"]";
		}
		return res;
	}
}
}