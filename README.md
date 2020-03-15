# parsing-tech

This is a repository for Marpa algorithm implementation in C#

# api

`soon` here will be full explanation of api for this lib

>**Main Objects**

  this objects you can use for describing your grammar

* Symbol

  _public String GetSymbolName()_

* Rule

  _public Symbol GetLeftHandSideOfRule()_
  
  _public List<Symbol> GetRightHandSideOfRule()_
  
  _public Symbol GetRightHandSideOfRule(int position)_
  
  _public Symbol GetRightHandSideOfRule(int position)_
  
  _public void AddToRightHandSideOfRule(List<Symbol> symbols)_
  
* Grammar
  
  _public void SetStartSym(Symbol StartSym)_
  
  _public Symbol GetStartSymbol()_

  _public int GetSymbolsListSize()_

  _public void AddSymbol(Symbol Symbol)_
  
  _public void AddSymbol(List<Symbol> Symbols)_
  
  _public int GetRulesListSize()_
  
  _public void AddRule(Rule Rule)_
  
  _public void AddRule(Symbol lhs, List<Symbol> rhs)_
  

>**Parser Objects**
* Recogniser

  _public Recogniser(Grammar grammar)_
  
  _public void Parse(String input)_

# extra

> Unseful Links
* original C implementation of the algorithm https://github.com/jeffreykegler/libmarpa 
* main Marpa paper https://jeffreykegler.github.io/Marpa-web-site/ 
* short Marpa overview http://blogs.perl.org/users/jeffrey_kegler/2011/11/what-is-the-marpa-algorithm.html 
* expanded Marpa overview https://docs.google.com/file/d/0B9_mR_M2zOc4Ni1zSW5IYzk3TGc/edit 
