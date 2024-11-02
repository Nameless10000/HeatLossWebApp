import {
  PageContainer,
  ProCard,
  ProColumns,
  ProForm,
  ProFormSelect,
  ProFormText,
  ProTable,
} from '@ant-design/pro-components';
import { request } from '@umijs/max';
import { Button, Divider, Flex, Typography, message } from 'antd';
import { useEffect, useState } from 'react';
import {
  Material,
  MaterialDTO,
  MaterialGroup,
  MaterialGroupDTO,
} from 'typings';

export default () => {
  const [materials, setMaterials] = useState<Material[]>([]);
  const [groups, setGroups] = useState<MaterialGroup[]>([]);

  useEffect(() => {
    request('/api/HeatLoss/GetMaterials').then((response: Material[]) =>
      setMaterials(response),
    );

    request('/api/HeatLoss/GetMaterialWithCounterGroups').then(
      (groups: MaterialGroup[]) => setGroups(groups),
    );
  }, []);

  const columns: ProColumns<Material>[] = [
    {
      search: false,
      title: 'Материал',
      dataIndex: 'name',
      align: 'center',
    },
    {
      search: false,
      title(schema, type, dom) {
        return (
          <>
            <Flex vertical style={{ width: '100%' }}>
              <Flex justify="center" align="center">
                <Typography.Text>
                  Корни полиномиала второй степени
                </Typography.Text>
              </Flex>
              <Divider />
              <Flex justify="space-evenly" align="center">
                <Typography.Text>A</Typography.Text>
                <Divider type="vertical" />
                <Typography.Text>B</Typography.Text>
                <Divider type="vertical" />
                <Typography.Text>C</Typography.Text>
              </Flex>
            </Flex>
          </>
        );
      },
      render(dom, material) {
        return (
          <>
            <Flex justify="space-around">
              <Typography.Text>
                {material.aCoeff.toExponential(2)}
              </Typography.Text>
              <Typography.Text>
                {material.bCoeff.toExponential(2)}
              </Typography.Text>
              <Typography.Text>
                {material.cCoeff.toExponential(2)}
              </Typography.Text>
            </Flex>
          </>
        );
      },
    },
    {
      title: 'Группа материалов',
      align: 'center',
      renderText(text, record) {
        return record.materialGroup.name;
      },
      sorter(a, b) {
        return a.materialGroup.id - b.materialGroup.id;
      },
      renderFormItem(schema, config, form, action) {
        return (
          <ProFormSelect
            placeholder={''}
            style={{ width: '25%' }}
            name="materialGroupID"
            options={groups.map((x) => ({ value: x.id, label: x.name }))}
          />
        );
      },
    },
    {
      title: '',
      align: 'center',
      render(dom, entity) {
        return (
          <>
            <Button
              danger
              type="primary"
              onClick={() => handleMaterialRemove(entity.id)}
            >
              Удалить
            </Button>
          </>
        );
      },
    },
  ];

  const materialGroupColumns: ProColumns<MaterialGroup>[] = [
    {
      title: 'Название группы',
      dataIndex: 'name',
    },
    {
      title: 'Количество материалов этого типа',
      dataIndex: 'materialsCount',
      width: '10%',
      align: 'center',
    },
    {
      align: 'center',
      render(dom, entity) {
        return (
          <>
            <Button
              danger
              type="primary"
              onClick={() => handleMaterialGroupRemove(entity.id)}
            >
              Удалить
            </Button>
          </>
        );
      },
      width: '20%',
    },
  ];

  const handleMaterialRemove = (id: number) => {
    request(`/api/HeatLoss/RemoveMaterial?materialId=${id}`, {
      method: 'DELETE',
    }).then((response: { message: string }) => message.info(response.message));

    setMaterials((prev) => prev.filter((x) => x.id != id));
  };

  const handleMaterialGroupRemove = (id: number) => {
    request(`/api/HeatLoss/RemoveMaterialGroup?materialGroupId=${id}`, {
      method: 'DELETE',
    }).then((response: { message: string }) => message.info(response.message));

    setGroups((prev) => prev.filter((x) => x.id != id));
  };

  const handleReset = () => {
    searchForm.resetFields();

    request(`/api/HeatLoss/GetMaterials?groupID=-1`).then(
      (response: Material[]) => setMaterials(response),
    );
  };

  const handleSearch = () => {
    const { materialGroupID } = searchForm.getFieldsValue();

    if (materialGroupID == undefined || materialGroupID == 0) return;

    request(`/api/HeatLoss/GetMaterials?groupID=${materialGroupID}`).then(
      (response: Material[]) => setMaterials(response),
    );
  };

  const returnEquation = () => {
    const { aCoeff, bCoeff, cCoeff } = materialForm.getFieldsValue();
    if (aCoeff == undefined || bCoeff == undefined || cCoeff == undefined)
      return;
    setEquation(
      `${aCoeff}x^2${bCoeff < 0 ? '' : '+'}${bCoeff}x${
        cCoeff < 0 ? '' : '+'
      }${cCoeff}`,
    );
  };

  const handleAddMaterialGroup = (materialGroupDTO: MaterialGroupDTO) => {
    request('/api/HeatLoss/AddMaterialGroup', {
      method: 'POST',
      data: materialGroupDTO,
    }).then((newGroup: MaterialGroup) => {
      if (newGroup.id != null) setGroups((prev) => [...prev, newGroup]);
      message.success(
        newGroup.message ?? 'Группа материалов успешно добавлена',
      );
    });
  };

  const handleAddMaterial = (materialDTO: MaterialDTO) => {
    request('/api/HeatLoss/AddMaterial', {
      method: 'POST',
      data: materialDTO,
    }).then((newMaterial: Material) => {
      setMaterials((prev) =>
        [...prev, newMaterial].sort(
          (a, b) => a.materialGroupID - b.materialGroupID,
        ),
      );
      message.success('Метариал успешно добавлен');
    });
  };

  const [searchForm] = ProForm.useForm<Material>();
  const [materialForm] = ProForm.useForm<MaterialDTO>();
  const [groupForm] = ProForm.useForm<MaterialGroupDTO>();
  const [equation, setEquation] = useState<string>();

  return (
    <PageContainer ghost title="Справочник материалов">
      <ProCard
        style={{
          borderRadius: '2em',
          boxShadow: '0 0 1em gray',
          padding: '1em',
          marginBottom: '2em',
        }}
      >
        <ProForm<MaterialDTO>
          onFinish={(materialDTO) => handleAddMaterial(materialDTO)}
          form={materialForm}
          onValuesChange={() => {
            returnEquation();
          }}
          submitter={{
            render(props, dom) {
              return [
                <Button
                  type="dashed"
                  onClick={() => materialForm.resetFields()}
                >
                  Сбросить
                </Button>,
                <Button type="primary" htmlType="submit">
                  {' '}
                  Добавить
                </Button>,
              ];
            },
          }}
        >
          <ProFormText
            name="name"
            label="Название"
            required
            rules={[
              {
                required: true,
              },
            ]}
            placeholder={''}
          />
          <Divider />
          <ProForm.Group style={{ width: '100%' }} align="center">
            <ProFormText
              style={{ width: 200 }}
              required
              name="aCoeff"
              label="A"
              placeholder={''}
              initialValue={0}
            />
            <ProFormText
              style={{ width: 200 }}
              required
              name="bCoeff"
              label="B"
              placeholder={''}
              initialValue={0}
            />
            <ProFormText
              style={{ width: 200 }}
              required
              name="cCoeff"
              label="C"
              placeholder={''}
              initialValue={0}
            />
            <Typography.Title level={5} style={{ height: '100%' }}>
              Итоговый вид уравнения: {equation}
            </Typography.Title>
          </ProForm.Group>
          <Divider />
          <ProFormSelect
            style={{ width: 200 }}
            label="Группа"
            required
            name="materialGroupID"
            options={groups.map((x) => ({ value: x.id, label: x.name }))}
          />
          <Divider />
        </ProForm>
      </ProCard>

      <ProCard
        style={{
          borderRadius: '2em',
          boxShadow: '0 0 1em gray',
          padding: '1em',
          marginBottom: '2em',
        }}
      >
        <ProForm
          onFinish={(groupDTO) => handleAddMaterialGroup(groupDTO)}
          form={groupForm}
          submitter={{
            render(props, dom) {
              return [
                <Button type="dashed" onClick={() => groupForm.resetFields()}>
                  Сбросить
                </Button>,
                <Button type="primary" htmlType="submit">
                  {' '}
                  Добавить
                </Button>,
              ];
            },
          }}
        >
          <ProFormText
            name="name"
            required
            label="Название группы"
            style={{ width: 200 }}
          />
        </ProForm>
      </ProCard>
      <ProCard
        style={{
          borderRadius: '2em',
          boxShadow: '0 0 1em gray',
          padding: '1em',
          marginBottom: '2em',
        }}
      >
        <ProTable<MaterialGroup>
          columns={materialGroupColumns}
          dataSource={groups}
          search={false}
          pagination={{
            pageSize: 10
          }}
        />
      </ProCard>
      <ProCard
        style={{
          borderRadius: '2em',
          boxShadow: '0 0 1em gray',
          padding: '1em',
        }}
      >
        <ProTable<Material>
          dataSource={materials}
          columns={columns}
          pagination={{
            pageSize: 10
          }}
          search={{
            form: searchForm,
            labelWidth: 'auto',
            optionRender(searchConfig, props, dom) {
              return [
                <Button type="dashed" onClick={handleReset}>
                  Сброс
                </Button>,
                <Button type="primary" onClick={handleSearch}>
                  Поиск
                </Button>,
              ];
            },
          }}
        />
      </ProCard>
    </PageContainer>
  );
};
