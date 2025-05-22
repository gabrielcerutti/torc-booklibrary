import * as React from 'react';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { Box, useMediaQuery, useTheme } from '@mui/material';
import type { Book } from '../types/Book';

interface BookListProps {
  books: Book[];
  loading: boolean;
}

const columns: GridColDef[] = [
  { field: 'title', headerName: 'Book Title', flex: 1, minWidth: 250 },
  {
    field: 'author',
    headerName: 'Author',
    flex: 1,
    minWidth: 180,
    valueGetter: (_value, row) => `${row.firstName} ${row.lastName}`,
  },
  { field: 'type', headerName: 'Type', flex: 1, minWidth: 100 },
  { field: 'isbn', headerName: 'ISBN', flex: 1, minWidth: 120 },
  { field: 'category', headerName: 'Category', flex: 1, minWidth: 100 },
  {
    field: 'availableCopies',
    headerName: 'Available Copies',
    flex: 1,
    minWidth: 140,
    valueGetter: (_value, row) =>
      `${row.copiesInUse}/${row.totalCopies}`,
  },
];

const BookList: React.FC<BookListProps> = ({ books, loading }) => {
  const theme = useTheme();
  const isSmallScreen = useMediaQuery(theme.breakpoints.down('md'));
  const height = isSmallScreen ? 'calc(100vh - 350px)' : 'calc(100vh - 200px)';
  return (
    <Box sx={{ width: '100%', mt: 1 }}>
      <div style={{
            display: 'flex',
            flexDirection: 'column',
            height
          }}>
        <DataGrid
          rows={books.map(b => ({ ...b, id: b.bookId }))}
          columns={columns}
          loading={loading}
          pageSizeOptions={[5, 10, 20]}
          initialState={{
            pagination: { paginationModel: { pageSize: 10, page: 0 } },
          }}
          disableRowSelectionOnClick          
        />
      </div>
    </Box>
  );
};

export default BookList;