import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Link from "@mui/material/Link";
import Typography from "@mui/material/Typography";
import SignInSignOutButton from "./SignInSignOutButton";
import { Link as RouterLink } from "react-router-dom";

const NavBar = () => {
    return (
        <div sx={{ flexGrow: 1 }}>
            <AppBar position="static">
            <Toolbar>
                <Typography sx={{ flexGrow: 1}}>
                    <Link component={RouterLink} to="/" color="inherit" variant="h6">AirBnbInsights</Link>
                    <Link component={RouterLink} to="/dashboard" color="inherit" variant="h6">Dashboard</Link>
                </Typography>
                {/* <WelcomeName /> */}
                <SignInSignOutButton />
            </Toolbar>
            </AppBar>
        </div>
    );
};

export default NavBar;