package hpcshelf.certification.web.service;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBElement;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller; 
import javax.xml.transform.Source;
import javax.xml.transform.stream.StreamSource;

import hpcshelf.certification.hash.component.*;
import hpcshelf.certification.lexical.analyzer.Lexer;
import hpcshelf.certification.lexical.analyzer.Token;
 
public class CertifierComponent {
	
	public static ArrayList<String> formalContracts = new ArrayList<String>(); 
	public static ArrayList<String> sourceCodes = new ArrayList<String>(); 
	
	public static ArrayList<String> formalContractsTokenized= new ArrayList<String>(); 
	
	public static ArrayList<String> formalContractsReplaced= new ArrayList<String>(); 
	public static ComponentType abstractComponent, concreteComponent;
	public static File file, file2 ;

	//public static String delimiters[]={"LP", "RP", "LB", "RB", "MD" };	
    public static void main(String args[]){
    	certify("/home/hpe/workspace/CertifierComponent/src/hpcshelf/certification/web/service/hpe.xml", "/home/hpe/workspace/CertifierComponent/src/hpcshelf/certification/web/service/hpe2.xml");	
    	    	
    		
    	}
    	
    	
    
	public static boolean certify(String hashAbstractComponent, String hashConcreteComponent) {

	        file = new File(hashAbstractComponent);
         	file2 = new File(hashConcreteComponent);
	
    		getFormalContracts();   
    		getSourceCodes();
    		performLexicalAnalysisFormalContracts();
    		searchForFormalContractInSource();
    		//performLexicalAnalysisSourceCode();
		return true;
	}
 
	public static void getFormalContracts(){
		try {
			JAXBContext jaxbContext = JAXBContext.newInstance(ComponentType.class);
    		Source source = new StreamSource(file);
    		Unmarshaller unmarshaller = jaxbContext.createUnmarshaller();
    		JAXBElement<ComponentType> root = unmarshaller.unmarshal(source, ComponentType.class);
    		ComponentType abstractComponent = root.getValue();
    		
    		//Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
    		//ComponentType component = (ComponentType) jaxbUnmarshaller.unmarshal(file);
    		
    		//Source source = new StreamSource(file);
    		//JAXBElement<ComponentType> root = jaxbUnmarshaller.unmarshal(source, ComponentType.class);
    		//FooClass foo = root.getValue();
    		
    		//System.out.println(CT.getHeader().isIsAbstract());
    		 Iterator itr3, itr2, itr = abstractComponent.getComponentInfo().getInnerComponentOrParameterOrSupplyParameter().iterator();
    		 Object element, element2, element3; 
    		 while(itr.hasNext()) {
    	          element = itr.next();
    	         //System.out.print(element + " classe "+ element.getClass().toString() + " \n");
    	         if (element.getClass()==InterfaceType.class){
    	        	// System.out.println("Source "+ ((InterfaceType)element).getSources().get(0).getFile().get(0).getContents());
    	        	 
    	        	 //formalContracts =  ((InterfaceType)element).getSources().get(0).getFile().get(0).getContents().
    	        	 //String text = ((InterfaceType)element).getSources().get(0).getFile().get(0).getContents();

    	        	  itr2 = ((InterfaceType)element).getSources().iterator();
    	    	     
    	        	  while(itr2.hasNext()) {
    	        		  element2 = itr2.next();
    	        		  
    	        	    SourceType st =  ((SourceType)element2);
    	        	    itr3 = st.getFile().iterator();
    	        	    while(itr3.hasNext()) {
      	        		  element3 = itr3.next();
      	        		  	String s = ((SourceFileType)element3).getContents();
      	        		  	Pattern p1 = Pattern.compile("\\@.*?\\@"); // "dotProduct ( .*? , .*? ) == dotProduct ( .*? , .*? )"
      	        		  	Matcher m = p1.matcher(s);
      	        		  	while(m.find()){
      	        		  		String b =  m.group();
      	        		  		formalContracts.add(b.substring(2, b.length()-2));
      	        		  	    System.out.println("Formal contract " + formalContracts.size()+ ": " + b.substring(2, b.length()-2));
  	        		  		
      	        		  	}
    	        	    }
    	        	    }
    	         }
    	      }
    	
    	  } catch (JAXBException e) {
    		e.printStackTrace();
    	  }		
	
		}
	public static void getSourceCodes(){
		try {
			JAXBContext jaxbContext = JAXBContext.newInstance(ComponentType.class);
    		Source source = new StreamSource(file2);
    		Unmarshaller unmarshaller = jaxbContext.createUnmarshaller();
    		JAXBElement<ComponentType> root = unmarshaller.unmarshal(source, ComponentType.class);
    		ComponentType abstractComponent = root.getValue();
    		
    		//Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
    		//ComponentType component = (ComponentType) jaxbUnmarshaller.unmarshal(file);
    		
    		//Source source = new StreamSource(file);
    		//JAXBElement<ComponentType> root = jaxbUnmarshaller.unmarshal(source, ComponentType.class);
    		//FooClass foo = root.getValue();
    		
    		//System.out.println(CT.getHeader().isIsAbstract());
    		 Iterator itr3, itr2, itr = abstractComponent.getComponentInfo().getInnerComponentOrParameterOrSupplyParameter().iterator();
    		 Object element, element2, element3; 
    		 while(itr.hasNext()) {
    	          element = itr.next();
    	         //System.out.print(element + " classe "+ element.getClass().toString() + " \n");
    	         if (element.getClass()==InterfaceType.class){
    	        	// System.out.println("Source "+ ((InterfaceType)element).getSources().get(0).getFile().get(0).getContents());
    	        	 
    	        	 //formalContracts =  ((InterfaceType)element).getSources().get(0).getFile().get(0).getContents().
    	        	 //String text = ((InterfaceType)element).getSources().get(0).getFile().get(0).getContents();

    	        	  itr2 = ((InterfaceType)element).getSources().iterator();
    	    	     
    	        	  while(itr2.hasNext()) {
    	        		  element2 = itr2.next();
    	        		  
    	        	    SourceType st =  ((SourceType)element2);
    	        	    itr3 = st.getFile().iterator();
    	        	    while(itr3.hasNext()) {
      	        		  element3 = itr3.next();
      	        		  	String s = ((SourceFileType)element3).getContents();
      	        		  
      	        		  sourceCodes.add(s);
      	        		System.out.println("Source Code " + sourceCodes.size()+ ": " + s);
      	        		  	/*
      	        		  	Pattern p1 = Pattern.compile("\\@.*?\\@");
      	        		  	Matcher m = p1.matcher(s);
      	        		  	while(m.find()){
      	        		  		String b =  m.group();
      	        		  	sourceCodes.add(b.substring(2, b.length()-2));
      	        		  	    System.out.println("Source Code " + sourceCodes.size()+ ": " + b.substring(2, b.length()-2));
      	        		  	    
      	        		  	    
  	        		  		
      	        		  	}*/
    	        	    }
    	        	    }
    	         }
    	      }
    	
    	  } catch (JAXBException e) {
    		e.printStackTrace();
    	  }		
	
		}

	public static void performLexicalAnalysisSourceCode(){
		
		// nao utilizadaaaaaaaaaaaaa
		//	String inFile = "Sample.in";
		//	String outFile = "Sample.out";
			
			Iterator itr = sourceCodes.iterator();
		     Object element;
		     Lexer lexer ;
		     Token t;
		     Map<String,String> tableVariableID = new HashMap<String,String>();
		     int numberID = 0;
		  
	  	  while(itr.hasNext()) {
	  		  element = itr.next();
	  		   String formalContractReplaced = "";
			lexer = new Lexer((String) element);

			
			String lexeme, token;
			
			while ((t = lexer.nextToken()) != null) {
				System.out.println("Lexema: "+t.getLexeme() + " Token: " + t.getTokenType());
				lexeme = t.getLexeme();
				token = t.getTokenType();
				
				if (token == "ID"){
					if (tableVariableID.containsKey(lexeme) == false){
						if ((t = lexer.nextToken()) != null) {
							
							if (t.getTokenType() == "LP"){
								System.out.println(  lexeme + " is a procedure, not a variable ");
								formalContractReplaced = formalContractReplaced + " " + lexeme + " (";
								
							}
							else{
								
								System.out.println(lexeme + " is a Variable not present in hashmap");
								tableVariableID.put(lexeme , token+numberID);
								System.out.println("Variable " + lexeme + " stored as " + tableVariableID.get(lexeme));
								
								formalContractReplaced = formalContractReplaced + " " + tableVariableID.get(lexeme) + " " + t.getLexeme();
								numberID++;
									
							}
							lexeme = t.getLexeme();
							token = t.getTokenType();
						}
						
						
					}else{
						formalContractReplaced = formalContractReplaced + " " + tableVariableID.get(lexeme);
					}
					
					
					
				}else{
					
					formalContractReplaced = formalContractReplaced + " " + lexeme;
				}
				
				

				
			}
			//System.out.println();
			System.out.println("Formal Contract replaced: "+ formalContractReplaced);
	  	  }
	  	
	  
			//writer.close(); 
			
			//System.out.println("Done tokenizing file: " + formalContracts.get(0));
			//System.out.println("Output written in file: " + outFile);

		
		
		}
	public static void performLexicalAnalysisFormalContracts(){
	//	String inFile = "Sample.in";
	//	String outFile = "Sample.out";
		
		Iterator itr = formalContracts.iterator();
	     Object element;
	     Lexer lexer ;
	     Token t;
	     Map<String,String> tableVariableID = new HashMap<String,String>();
	     int numberID = 0;
	     boolean isDoubleEq;
	  
  	  while(itr.hasNext()) {
  		  element = itr.next();
  		String whiteSpaces="\\s*";
  		whiteSpaces="";
  		   String formalContractTokenized = "", formalContractReplaced = whiteSpaces;
  		 String bars= "";
		lexer = new Lexer((String) element);

		
		String lexeme, token, coringa = ".*?", coringa2= ".*?";
		
		while ((t = lexer.nextToken()) != null) {
			System.out.println("Lexema: "+t.getLexeme() + " Token: " + t.getTokenType());
			lexeme = t.getLexeme();
			token = t.getTokenType();
			isDoubleEq = false;
			switch(t.getTokenType()){
			case "LP":
					bars = "\\";
					break;
			case "RP":
				bars = "\\";
					break;
			case "LB":
				bars = "\\";
					break;
			case "RB":
				bars = "\\";
					break;
			case "MO":
				bars = "\\";
				isDoubleEq = true;
					break;
			case "TO":
				bars = "\\";
				break;
			case "EQ":
				bars = "\\";
				isDoubleEq = true;
				break;
			case "PO":
				bars = "\\";
				break;
			default:
				bars="";	
				break;
			
			}
						
			if (token == "ID"){
				// is an ID
				if (tableVariableID.containsKey(lexeme) == false){
					if ((t = lexer.nextToken()) != null) {
						isDoubleEq = false;
						switch(t.getTokenType()){
						case "LP":
								bars = "\\";
								break;
						case "RP":
							bars = "\\";
								break;
						case "LB":
							bars = "\\";
								break;
						case "RB":
							bars = "\\";
								break;
						case "MO":
							bars = "\\";
								break;
						case "TO":
							bars = "\\";
								break;
						case "PO":
							bars = "\\";
								break;
						case "EQ":
							bars = "\\";
							isDoubleEq = true;
								break;
								
						default:
								bars="";
								break;
							
						
						}
						if (t.getTokenType() == "LP"){ // Token following ID is a LP
							System.out.println(  lexeme + " is a procedure, not a variable ");
							formalContractTokenized = formalContractTokenized + lexeme + "(";
							if (formalContractReplaced == whiteSpaces){
							formalContractReplaced =formalContractReplaced + lexeme + whiteSpaces + bars + "(" + whiteSpaces;
							}else{
								formalContractReplaced =formalContractReplaced + lexeme + whiteSpaces + bars + "(" + whiteSpaces;
								
							}
						}
						else{// Token following ID is not a LP
							
							System.out.println(lexeme + " is a Variable not present in hashmap");
							tableVariableID.put(lexeme , token+numberID);
							System.out.println("Variable " + lexeme + " stored as " + tableVariableID.get(lexeme));
							
							formalContractTokenized = formalContractTokenized  + tableVariableID.get(lexeme) +  t.getLexeme();
							
							if(isDoubleEq){ // Token following ID is a ==
								if (formalContractReplaced == whiteSpaces){
									coringa = lexeme;
									formalContractReplaced = coringa +  bars+  "=\\=" + whiteSpaces;
									
								}else{
								formalContractReplaced =formalContractReplaced  + coringa +  bars+  "=\\=" + whiteSpaces;
								}
								}else{//token following ID is not a ==
									coringa = lexeme;
									if (formalContractReplaced == whiteSpaces){
										if(t.getTokenType() == "IN"){
									formalContractReplaced =  coringa +  bars+  coringa2 + whiteSpaces;
										}else{
											formalContractReplaced =  coringa +  bars+  t.getLexeme() + whiteSpaces;
											
										}
									}else{
										if(t.getTokenType() == "IN"){
										formalContractReplaced = formalContractReplaced + coringa +  bars+  coringa2 + whiteSpaces;
										}else{
											
											formalContractReplaced = formalContractReplaced + coringa +  bars+  t.getLexeme() + whiteSpaces;
										}
									}
									
								
								}
							
							//if(numberID==numberID);
							// "dotProduct\\(.*?,.*?\\) \\=\\= dotProduct\\(.*?,.*?\\)"
							numberID++;
								
						}
						lexeme = t.getLexeme();
						token = t.getTokenType();
					}
					
					
				}else{ // ID already present in table
					formalContractTokenized = formalContractTokenized  + tableVariableID.get(lexeme);
					coringa = lexeme;
					if (formalContractReplaced == whiteSpaces){
					formalContractReplaced = coringa;
					}else{
						formalContractReplaced = formalContractReplaced +  coringa;
						
					}
					
				
				}
				
				
				
			}else{
				// it is not an ID
				formalContractTokenized = formalContractTokenized +  lexeme;
				if(isDoubleEq){
					if (formalContractReplaced == whiteSpaces){	
							formalContractReplaced = bars +"=\\=" + whiteSpaces;
					}else{
						formalContractReplaced = formalContractReplaced + bars +"=\\=" + whiteSpaces;
						
					}
					
				}
				else{// nor ID nor ==
					// nor ID nor ==	
					if (formalContractReplaced == whiteSpaces){
					if(token == "IN"){
						formalContractReplaced = bars +coringa2 + whiteSpaces;
					}else{
						formalContractReplaced = bars +lexeme + whiteSpaces;
						
					}
					}else{
						if(token == "IN"){
						formalContractReplaced = formalContractReplaced + bars +coringa2 + whiteSpaces;
						}else{
							
							formalContractReplaced = formalContractReplaced + bars +lexeme + whiteSpaces;
							
						}
						
					}
				}
			}
			
			

			
		}
		//System.out.println();
		System.out.println("Formal Contract tokenized: "+ formalContractTokenized);
		formalContractsTokenized.add(formalContractTokenized);
		
		System.out.println("Formal Contract replaced: "+ formalContractReplaced);
		formalContractsReplaced.add(formalContractReplaced);
  	  }
  	
  
		//writer.close(); 
		
		//System.out.println("Done tokenizing file: " + formalContracts.get(0));
		//System.out.println("Output written in file: " + outFile);

	
	
	}
	public static void searchForFormalContractInSource(){
		Iterator itr2 = formalContractsReplaced.iterator();
	     Object element2;
	     String formalContractRegExp ;
	     while(itr2.hasNext()) {
	  		  element2 = itr2.next();
	  		//System.out.println("Formal Contract replaced: " + (String) element2);
	  		formalContractRegExp = (String) element2;
	  		//formalContractRegExp = "dotProduct\\(.*?\\*.*?,.*?\\)\\=\\=dotProduct\\(.*?,.*?\\)";
	  		//formalContractRegExp = "\\s*dotProduct\\s*\\(\\s*.*?\\+\\s*.*?,\\s*.*?\\)\\s*\\=\\=\\s*dotProduct\\s*\\(\\s*.*?\\*\\s*.*?,\\s*.*?\\)\\s*";
	  		System.out.println("Formal contract regular exp :" +formalContractRegExp+"!!!");
	  		
		Iterator itr = sourceCodes.iterator();
		
	     Object element;
	     while(itr.hasNext()) {
	  		  element = itr.next();
	  		                        //\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*\\=\\=\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*
		//Pattern p1 = Pattern.compile("\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*\\=\\=\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*"); // "dotProduct ( .*? , .*? ) == dotProduct ( .*? , .*? )"
	  		//formalContractRegExp = "\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*\\=\\=\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*";
	  		//System.out.println("Formal contract regular exp bef:" +formalContractRegExp+"!!!");
	  	//formalContractRegExp = "\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*\\=\\=\\s*dotProduct\\s*\\(\\s*.*?,\\s*.*?\\)\\s*";
	  		//System.out.println("Formal contract regular exp aft:" +formalContractRegExp+"!!!");
	    Pattern p1 = Pattern.compile(formalContractRegExp);
	  		  Matcher m = p1.matcher((String) element);
		  	while(m.find()){
		  		String b =  m.group();
		  		//formalContracts.add(b.substring(2, b.length()-2));
		  	    System.out.println("Found pattern of formal contract in source code " + b);
	  		
		  	}
	     }
	     }

	}}
