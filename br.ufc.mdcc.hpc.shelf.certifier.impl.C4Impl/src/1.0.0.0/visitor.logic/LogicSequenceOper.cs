using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System;
using br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{

public class LogicSequenceOper : AbstractCertifierElementLogic{

	public override int logic(CertifierOrchestrationElement element) {
		int ret=CertifierConstants.NORMAL_SIGNAL;
		CertifierConsoleLogger.write("STARTING SEQUENCE TASKS");

		for(int i=element.getChildren().Count-1;i>=0;i--){
			CertifierOrchestrationElement child = element.getChildren()[i];
			ret = child.run();
			if(ret>CertifierConstants.NORMAL_SIGNAL){ /*=CertifierConstants.BREAK_SIGNAL ||
					ret==CertifierConstants.CONTINUE_SIGNAL ||ret==CertifierConstants.RETURN_CERTIFIED ||ret==CertifierConstants.RETURN_NOT_CERTIFIED){
				*/
					CertifierConsoleLogger.write("Signal found: " + ret + ". Stopping sequence execution.");
				break;
				
			}
		}
		CertifierConsoleLogger.write("ENDED SEQUENCE TASKS");
	return ret;
	}

}
}