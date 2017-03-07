using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System;
using br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{

/**
 * Implements the logic for ACTIVATE/INVOKE operation.
 * [activate/invoke]
 *  |
 *  |---handle_id(?)---|--instantiate [comp_id]
 *  				   |--prove [action_id]
 *  
 * @author allberson
 *
 */
public class LogicPerformOper : AbstractCertifierElementLogic{

		public override int logic(CertifierOrchestrationElement element) {
			
			Console.WriteLine (" element children 0 operation " + element.getElement().GetType());
		
			XMLCertifierAction action  = (XMLCertifierAction)element.getElement();
		String action_id = null; // action_id or component_id
		String action_oper = null;
			string comp_id = null;
		action_id = action.action_id;

		//action_oper = action..getAction().value();
			action_oper = action.action.ToString();
			/*Console.WriteLine ("passei aqui logic perform action " + action.valueField + action.action_id + 
				action.action.ToString() + action.oper_name + action.action_idField1 + action.actionField.ToString() + 
				action.comp_id + action.comp_idField1
				+ " action action " + action.action.ToString());*/


			comp_id = action.comp_id;
			Console.WriteLine (" LogicPerformOper action id " + action_id 
			+  " comp id " +
				comp_id+ " action oper " + 
				action_oper );

		if (action_oper.Equals("instantiate")) {
			this.instatiateOper(comp_id);
		} else if (action_oper.Equals("compute")) {
			this.computeOper(comp_id, action_id);
		} /*else if (action_oper.Equals("compute")) {
			this.computeOper(subject_id);
		}*/
	return CertifierConstants.NORMAL_SIGNAL;
	}
	
	private void instatiateOper(String compId) {

		/*ArchComponent archComponent = HPCStormObjectRepository
				.getWorkflowEngine().getArchComponentByID(
						Integer.parseInt(compId));
		System.out.println("invoke instantiate=> archComponent: ["+archComponent.getId()+"]" + archComponent.getName());*/
		CertifierConsoleLogger.write("perform instantiate => " + "CompID " + compId);
		
			string handle_fake = "handle perform " + compId;
		LogicActionInstantiate l;
		//	if(!C4Impl.InstantiateActions.ContainsKey(handle_fake)){
				l =  new LogicActionInstantiate(compId,handle_fake);
		
				C4Impl.InstantiateActions.TryAdd(handle_fake, l);
			
	/*	}else{
			
			l= (LogicActionInstantiate)C4Impl.InstantiateActions[compId];
			//l =  new LogicActionInstantiate(compId, C4Impl.tacticalCommand);
		}*/
		 
		
		l.run();
		
		
	}

	/*private void resolveOper(String compId) {

		ArchComponent archComponent = HPCStormObjectRepository
				.getWorkflowEngine().getArchComponentByID(
						Integer.parseInt(compId));
		System.out.println("invoke resolve=> archComponent: ["+archComponent.getId()+"]" + archComponent.getName());

	}*/

	private void computeOper(String compId, string actionId) {
		/*ArchAction archAction = HPCStormObjectRepository.getWorkflowEngine()
				.getArchActionId(Integer.parseInt(actionId));
		System.out.println("invoke compute=> archAction: ["+archAction.getId()+"]" + archAction.getName());
		
	*/string handle_fake = "handle perform " + compId + " " + actionId;
			CertifierConsoleLogger.write("perform compute => " + "CompID " + compId + " actId " + actionId);
		LogicActionCompute l;
		//	if(!C4Impl.ComputeActions.ContainsKey(handle_fake)){
			//if (compId.Equals("79")){ // call alt-ergo
				l =  new LogicActionCompute(compId, actionId,handle_fake);
			//	else{// call z3
					
			//		l =  new LogicActionCompute(compId, actionId, C4Impl.tacticalCommand2);
		//		}
			
			
			C4Impl.ComputeActions.TryAdd(handle_fake, l);
			
	/*	}else{
			l= (LogicActionCompute)C4Impl.ComputeActions[compId];
			
		}
		 
	*/	
		l.run();
		
	
		
		
		
		
	}
	}}
