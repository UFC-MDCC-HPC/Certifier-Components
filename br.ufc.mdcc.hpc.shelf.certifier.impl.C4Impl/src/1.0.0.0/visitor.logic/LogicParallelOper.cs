using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.grammar;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.util;
using br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.action.logic;
using System;
using br.ufc.mdcc.hpc.shelf.certifier.impl.C4Impl;
namespace br.ufc.mdcc.hpcshelf.certifier.impl.computation.CertfierImpl.visitor.logic{

public class LogicParallelOper : AbstractCertifierElementLogic{
	bool hasBreakOrContinue=false; int ret;
		public override int logic(CertifierOrchestrationElement element) {
		
		ParallelJob[] threads = new ParallelJob[element.getChildren().Count];
		
		//creating threads
		for(int i=element.getChildren().Count-1, j=0;i>=0;i--,j++){
			CertifierOrchestrationElement child = element.getChildren()[i];
			ParallelJob pj = new ParallelJob(child);
			threads[j] = pj;
		}
		
		//starting threads
		CertifierConsoleLogger.write("STARTING PARALLEL TASK");
		foreach(ParallelJob job in threads){
			job.run();
		
		}
			
		
		//join threads
		foreach(ParallelJob job in threads){
			
				
				if(!hasBreakOrContinue){
				job._thread.Join();
				ret = job.ret;
					Console.WriteLine (" parallel job. return of job " + ret);
					if(ret > CertifierConstants.NORMAL_SIGNAL ){//== CertifierConstants.BREAK_SIGNAL || ret == CertifierConstants.CONTINUE_SIGNAL || ret == CertifierConstants.RETURN_CERTIFIED||ret == CertifierConstants.RETURN_NOT_CERTIFIED ){
					
						hasBreakOrContinue=true;
						//break;
					}
					
				}else{
					CertifierConsoleLogger.write("Signal found in parallel tasks: "+ ret + ". Interrupting all the remaining threads.");
					job._thread.Interrupt();
					
				}
				
			
		}
		CertifierConsoleLogger.write("ENDED PARALLEL TASK");
		
		return ret;
		
	}

}


}
