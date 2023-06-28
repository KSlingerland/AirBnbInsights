import { Routes, Route, useNavigate } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "react-query";

// MSAL imports
import { MsalProvider } from "@azure/msal-react";
import { CustomNavigationClient } from "./utils/NavigationClient";

//App pages import
import Listing from './components/Listing';
import Root from './pages/root';
import Dashboard from "./pages/Dashboard";
import { Profile } from "./pages/Profile";
import NavBar from "./components/ui-components/NavBar";

function App({ pca }) {

  // The next 3 lines are optional. This is how you configure MSAL to take advantage of the router's navigate functions when MSAL redirects between pages in your app
  const navigate = useNavigate();
  const navigationClient = new CustomNavigationClient(navigate);
  pca.setNavigationClient(navigationClient);

  const queryClient = new QueryClient()

  return (
    <QueryClientProvider client={queryClient}>
      <MsalProvider instance={pca}>
        <NavBar />
        <Pages />
      </MsalProvider>
    </QueryClientProvider>
  )
}

function Pages() {
  return (
      <Routes>
        <Route path="/dashboard" element={<Dashboard />}/>
        <Route path="/profile" element={<Profile />} />
        <Route path="/" element={<Root />}>
          <Route path="listing/:listingid" element={<Listing />}/>
        </Route>
      </Routes>
  )
}

export default App;
