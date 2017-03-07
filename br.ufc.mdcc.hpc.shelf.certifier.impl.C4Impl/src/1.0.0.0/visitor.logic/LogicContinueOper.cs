using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using  br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{



public class LogicContinueOper : AbstractCertifierElementLogic{

		public override int logic(CertifierOrchestrationElement element) {
		CertifierConsoleLogger.write("CONTINUE OPERATION");
		return CertifierConstants.CONTINUE_SIGNAL;
		
		//C4Impl.BreakIterate = true;
		
		
	}

}
}