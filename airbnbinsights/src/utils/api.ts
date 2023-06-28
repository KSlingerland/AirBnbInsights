import axios from "axios";

const client = axios.create({
    // baseURL:
    baseURL: `${process.env.REACT_APP_API}`,
  });

  const requests = {
    get: async (url: string) => {
      try {
        const response = await client.get(url);
        return response.data;
      } catch (err) {
        throw err;
      }
    },
  };
  
  class ApiService {
    client: typeof requests;
  
    constructor() {
      this.client = requests;
    }
  
    dashboard = {
      get: () => this.client.get("/charts/neighbourhoods"),
    };
  
    listing = {
      getAll: async () => this.client.get("/listings"),
      get: async (id: string) => this.client.get(`/listings/${id}`),
    };
  }
  
  export default new ApiService();