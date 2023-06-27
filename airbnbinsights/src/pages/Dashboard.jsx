import {
    AuthenticatedTemplate,
    UnauthenticatedTemplate,
  } from "@azure/msal-react";
import Charts from '../components/Charts'
import SignInSignOutButton from "../components/ui-components/SignInSignOutButton";

const Dashboard = () => {
    // const Authenticated = useIsAuthenticated();
    return (
        <>
            <AuthenticatedTemplate>
                <h1>Overview</h1>
                <Charts />
            </AuthenticatedTemplate>
            <UnauthenticatedTemplate>
                <SignInSignOutButton/>
                <h5>Om deze pagina te bekijken moet je inlogd zijn</h5>
            </UnauthenticatedTemplate>
        </>
    )
}

export default Dashboard;