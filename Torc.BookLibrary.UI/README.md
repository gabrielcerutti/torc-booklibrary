# Torc Book Library UI

This is a React + TypeScript frontend for the Royal Library book management system. It allows you to search and browse your personal book library.

## Features

- Search books by Author, ISBN, or Ownership Status
- View results in a modern, sortable, paginated data grid (MUI DataGrid)
- Responsive and clean UI using Material UI

## Prerequisites

- [Node.js](https://nodejs.org/) (v18 or newer recommended)
- [npm](https://www.npmjs.com/) (comes with Node.js)
- The backend API running and accessible at `http://localhost:8010/api/Books`

## Getting Started

1. **Clone the repository**

   ```sh
   git clone <your-repo-url>
   cd Torc.BookLibrary.UI
   ```

2. **Install dependencies**

   ```sh
   npm install
   ```

3. **Start the development server**

   ```sh
   npm run dev
   ```

4. **Open the app**

   Visit [http://localhost:5173](http://localhost:5173) in your browser (or the URL shown in your terminal).

## Configuration

- The frontend expects the backend API to be running at `http://localhost:8010/api/Books`.
- If your backend runs on a different URL or port, update the API base URL in `src/api/BookApi.ts`.

## Build for Production

```sh
npm run build
```

The production-ready files will be in the `dist` folder.

## Linting

```sh
npm run lint
```

## Tech Stack

- [React](https://react.dev/)
- [TypeScript](https://www.typescriptlang.org/)
- [Material UI](https://mui.com/)
- [MUI Data Grid](https://mui.com/x/react-data-grid/)
- [Vite](https://vitejs.dev/)

## License

MIT