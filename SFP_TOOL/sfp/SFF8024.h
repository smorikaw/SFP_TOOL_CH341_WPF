#pragma once
///////////////////////////////////////////]
//
#include	<string>

public ref class SFF8024 {
public:
	std::string ident(int code);
	std::string connector_type(int code);
	// SFF8024 tabel 4-4
	std::string exttype(int code);
	// sff8024_table Table 4 - 5 Host Electrical Interface IDs
	std::string tech(int code);
	// sff8024_table Table 4 - 5 Host Electrical Interface IDs
	std::string ehint(int code);
	// sff8024_table Table 4 - 5 Host Electrical Interface IDs
	std::string smfint(int code);
	// sff8024_table Table 4 - 5 Host Electrical Interface IDs
	std::string mmfint(int code);
	std::string optype(int code);
};
