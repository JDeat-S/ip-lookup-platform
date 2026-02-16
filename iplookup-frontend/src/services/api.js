import axios from "axios";

/* const api = axios.create({
  baseURL: "https://localhost:44352/api", //Cambia el puerto o/y IP por el de tu API
}); */
const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});
export const queryIp = (ip) => {
  return api.post("/ip", ip, {
    headers: { "Content-Type": "application/json" },
  });
};

export const getAllIps = () => {
  return api.get("/ip");
};

export const deleteIp = (id) => {
  return api.delete(`/ip/${id}`);
};
