import { useState } from "react";
import IpForm from "./components/IpForm";
import IpTable from "./components/IpTable";

function App() {
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  const refreshTable = () => {
    setRefreshTrigger(prev => prev + 1);
  };

  return (
    <div className="container py-5">
      <div className="text-center mb-4">
        <h1 className="fw-bold">IP Lookup Platform</h1>
      </div>

      <IpForm onSuccess={refreshTable} />
      <IpTable refreshTrigger={refreshTrigger} />
    </div>
  );
}

export default App;
