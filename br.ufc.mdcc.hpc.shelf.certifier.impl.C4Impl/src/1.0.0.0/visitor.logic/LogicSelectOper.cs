using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System;
using br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{


public class LogicSelectOper : AbstractCertifierElementLogic{
		public override int logic(CertifierOrchestrationElement element) {
			int ret=CertifierConstants.NORMAL_SIGNAL;
	
			CertifierConsoleLogger.write("STARTING SELECT TASK");

			selectoperComplexType select_oper = (selectoperComplexType)element.getElement();
			String chosen = select_oper.chosen;

			for(int i=element.getChildren().Count-1;i>=0;i--){
				CertifierOrchestrationElement select = element.getChildren()[i];
				selectoperComplexTypeChoice choice_oper = (selectoperComplexTypeChoice)select.getElement();
				String action_id = choice_oper.action_id;
				//navegar no select
				if (string.Equals(action_id, chosen, StringComparison.OrdinalIgnoreCase)) {
					foreach (CertifierOrchestrationElement child in select.getChildren()) {

						ret = child.run ();
						if(ret>CertifierConstants.NORMAL_SIGNAL){ /*=CertifierConstants.BREAK_SIGNAL ||
					ret==CertifierConstants.CONTINUE_SIGNAL ||ret==CertifierConstants.RETURN_CERTIFIED ||ret==CertifierConstants.RETURN_NOT_CERTIFIED){
				*/
							CertifierConsoleLogger.write("Signal found: " + ret + ". Stopping sequence execution.");
							break;

						}
						}
				}
			}


			CertifierConsoleLogger.write("ENDED SELECT TASK");



			return ret;


		}
	
	}


}