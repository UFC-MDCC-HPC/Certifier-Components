using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using System;

namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{



public class LogicBreakOper : AbstractCertifierElementLogic{

	
		public  override int logic(CertifierOrchestrationElement element) {
		Console.WriteLine("BREAK OPERATION");
		return CertifierConstants.BREAK_SIGNAL;
		
		//C4Impl.BreakIterate = true;
		
		
	}

}
}