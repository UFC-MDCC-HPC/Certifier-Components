using System.Collections.Generic;
using System.Collections;
using System;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor {




public abstract class AbstractCertifierElementLogic {

	private  IDictionary<String, Object> variables;
	
	public AbstractCertifierElementLogic() {
		this.variables = new Dictionary<String, Object>();
	}
	
	public abstract int logic(CertifierOrchestrationElement element);
	 
	public void addVariable(String name, Object value){
		this.variables.Add(name, value);
	}
	
	public Object getVariable(String name){
			Object ret;
		 this.variables.TryGetValue(name, out ret);
			return ret;
		}
}
}