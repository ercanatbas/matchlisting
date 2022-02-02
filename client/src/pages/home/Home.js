import React, { useState } from 'react';
import PropTypes from 'prop-types';
import PageLayout from '../../layout/PageLayout';
import { Upload, Button, Empty, message } from 'antd';
import { UploadOutlined } from '@ant-design/icons';

function Home({ location: { pathname } }) {
  if (pathname !== '/') {
    return null;
  }

  const [uploading, setUploading] = useState(false);
  const [fileList, setFile] = useState([]);

  const handleUpload = () => {
    const formData = new FormData();
    formData.append('file', fileList[0]);
    setUploading(true);

    fetch('http://localhost:5200/api/v1/Upload/files', {
      method: 'POST',
      body: formData,
    })
      .then((response) => {
        if (!response.ok) {
          response.json().then((json) => {
            json.errors.map((error) => message.error(error.message));
          });
        } else {
          message.success('upload successfully.');
        }
      })
      .catch(() => {
        message.error('upload failed.');
      })
      .finally(() => {
        setFile([]);
        setUploading(false);
      });
  };

  const props = {
    accept: '.xls, .xlsx,.json,.xml',
    maxCount: 1,
    listType: 'picture',
    onRemove: (file) => {
      console.log(file);
      setFile([...fileList.filter((x) => x.uid !== file.uid)]);
    },
    beforeUpload: (file) => {
      console.log(file);
      const acceptedFiles =
        file.type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' ||
        file.type === 'text/xml' ||
        file.type === 'application/json';
      if (!acceptedFiles) {
        message.error('You can only upload XLS/XLSX/JSON/XML file!');
      } else {
        setFile([file]);
      }
      return false;
    },
    fileList,
  };

  return (
    <PageLayout title="Home">
      <Empty
        image="https://gw.alipayobjects.com/zos/antfincdn/ZHrcdLPrvN/empty.svg"
        imageStyle={{
          height: 100,
          marginBottom: 50,
        }}
        description={<span>Upload Match</span>}
      >
        <Upload {...props}>
          <Button icon={<UploadOutlined />}>Select File</Button>
        </Upload>
        <Button type="primary" onClick={handleUpload} loading={uploading} style={{ marginTop: 16 }}>
          {uploading ? 'Uploading' : 'Start Upload'}
        </Button>
      </Empty>
    </PageLayout>
  );
}

Home.propTypes = {
  location: PropTypes.object.isRequired,
};

export default Home;
