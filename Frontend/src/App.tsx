import { AppRoutes } from "./routes";
import { ThemeProvider } from "@mui/material/styles";
import { CssBaseline } from "@mui/material";
import { theme } from "./theme";
import { GlobalErrorModal } from "./components/common/GlobalErrorModal";

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <AppRoutes />
      <GlobalErrorModal />
    </ThemeProvider>
  );
}

export default App;