using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System;
using br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{

/**
 * Implements the logic for WAIT operation.
 * [wait]
 *  |
 *  |---handle_id(?)---|--instantiate [comp_id]
 *  				   |--prove [action_id]
 *  
 * @author allberson
 *
 */
public class LogicWaitOper : AbstractCertifierElementLogic {

		public override int logic(CertifierOrchestrationElement element) {
		String handle_id = null;
		String action_id = null; // action_id or component_id
		String action_oper = null;
		String comp_id = null;

			if ( typeof( XMLCertifierPrimOper).IsInstanceOfType(element.getElement())) {
			XMLCertifierPrimOper wait_oper = (XMLCertifierPrimOper) element.getElement();
			handle_id = wait_oper.handle_id;
		}

		for (int i = element.getChildren().Count - 1; i >= 0; i--) {
			CertifierOrchestrationElement child = element.getChildren()[i];
				if ( typeof(XMLCertifierAction).IsInstanceOfType(child.getElement())) {
				XMLCertifierAction safe_action = (XMLCertifierAction) child.getElement();
				comp_id = safe_action.comp_id;
					action_id = safe_action.action_id;
				action_oper = safe_action.action.ToString();
			}
		}

		if (action_oper.Equals("instantiate")) {
				this.instantiateOper(handle_id);
		} else if (action_oper.Equals("compute")) {
				this.computeOper(handle_id);
		} /*else if (action_oper.Equals("compute")) {
			this.computeOper(subject_id);
		}*/
    return CertifierConstants.NORMAL_SIGNAL;
	}

		private void instantiateOper(String handle_id) {

		/*ArchComponent archComponent = HPCStormObjectRepository
				.getWorkflowEngine().getArchComponentByID(
						Integer.parseInt(compId));
		System.out.println("wait instantiate=> archComponent: ["+archComponent.getId()+"]" + archComponent.getName());
	*/
			CertifierConsoleLogger.write("wait instantiate => " + "HandleID" + handle_id);
		
		LogicActionInstantiate l=null;
			if(!C4Impl.InstantiateActions.ContainsKey(handle_id)){
				CertifierConsoleLogger.write("Fatal Error: Waiting for inexistent handleID. handle_id: " + handle_id);
			System.Environment.Exit(0);
		}else{
				l= (LogicActionInstantiate)C4Impl.InstantiateActions[handle_id];
			
		}
		 
		
		
			l._thread.Join();
		
		//System.out.println("ACTION! " + "instantiate "+ compId);
		
	}

	/*private void resolveOper(String compId) {

		ArchComponent archComponent = HPCStormObjectRepository
				.getWorkflowEngine().getArchComponentByID(
						Integer.parseInt(compId));
		System.out.println("wait resolve=> archComponent: ["+archComponent.getId()+"]" + archComponent.getName());

	}*/

		private void computeOper(String handle_id) {
		/*ArchAction archAction = HPCStormObjectRepository.getWorkflowEngine()
				.getArchActionId(Integer.parseInt(actionId));
		System.out.println("wait compute=> archAction: ["+archAction.getId()+"]" + archAction.getName());
	*/
			CertifierConsoleLogger.write("wait compute => " + "handleId " + handle_id);
		LogicActionCompute l=null;
			if(!C4Impl.ComputeActions.ContainsKey(handle_id)){
				CertifierConsoleLogger.write("Fatal Error: Waiting for inexistent handleid. handle_id: " + handle_id);
			System.Environment.Exit(0);
		}else{
				l= (LogicActionCompute)C4Impl.ComputeActions[handle_id];
			
		}
		 
		
	
			l._thread.Join();
		
		
		//System.out.println("ACTION! " + "prove "+ actionId);
	
	}
}
}