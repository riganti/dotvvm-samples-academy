syntax highlighting 
https://github.com/gpoore/minted

## instal python 3.5 
## run cmd 
pip3.exe install Pygments
## update
python -m pip install --upgrade pip

Build/define Output Profiles / Command line arguments to pass to the compiler
-shell-escape -synctex=-1 -interaction=nonstopmode  "%pm"

## to
Build/define Output Profiles/Viewer/ Executable path
"C:\Program Files\SumatraPDF\SumatraPDF.exe" -inverse-search "\"c:\Program Files\TeXnicCenter\TeXnicCenter.exe\" /ddecmd \"[goto('%f', '%l')]\""