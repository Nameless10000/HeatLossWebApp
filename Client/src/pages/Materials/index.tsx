import {
  PageContainer,
  ProCard,
  ProColumns,
  ProForm,
  ProFormDigit,
  ProFormSelect,
  ProFormText,
  ProTable,
} from '@ant-design/pro-components';
import { request } from '@umijs/max';
import { Button, Checkbox, Divider, Flex, Typography } from 'antd';
import { useEffect, useState } from 'react';
import { Material, MaterialDTO, MaterialGroup } from 'typings';
import { PlusOutlined } from '@ant-design/icons'

export default () => {
  const [materials, setMaterials] = useState<Material[]>([]);
  const [groups, setGroups] = useState<{value: number, label: string}[]>([]);
  const [isNewGroup, setIsNewGroup] = useState<boolean>(false);

  useEffect(() => {
    request('http://localhost:5114/api/HeatLoss/GetMaterials').then(
      (response: Material[]) => setMaterials(response),
    );

    request(
        'http://localhost:5114/api/HeatLoss/GetMaterialGroups',
      ).then((groups: MaterialGroup[]) => setGroups(groups.map(mg => ({ value: mg.id, label: mg.name }))))
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
            options={groups}
          />
        );
      },
    },
  ];

  const handleReset = () => {
    searchForm.resetFields();

    request(`http://localhost:5114/api/HeatLoss/GetMaterials?groupID=-1`).then(
      (response: Material[]) => setMaterials(response),
    );
  };

  const handleSearch = () => {
    const { materialGroupID } = searchForm.getFieldsValue();

    if (materialGroupID == undefined || materialGroupID == 0) return;

    request(
      `http://localhost:5114/api/HeatLoss/GetMaterials?groupID=${materialGroupID}`,
    ).then((response: Material[]) => setMaterials(response));
  };

  const returnEquation = () => {
    const {aCoeff, bCoeff, cCoeff} = form.getFieldsValue();
    if (aCoeff == undefined || bCoeff == undefined || cCoeff == undefined) return;
    setEquation(`${aCoeff}x^2${bCoeff < 0 ? '' : '+'}${bCoeff}x${cCoeff < 0 ? '' : '+'}${cCoeff}`);
  } 

  const handleAddMaterial = (materialDTO: MaterialDTO) => {
        request("http://localhost:5114/api/HeatLoss/AddMaterial", {
            method: "POST",
            data: materialDTO
        })
        .then((newMaterial: Material) => setMaterials(prev => [...prev, newMaterial].sort((a, b) => a.materialGroupID - b.materialGroupID)))
  }

  const [searchForm] = ProForm.useForm<Material>();
  const [form] = ProForm.useForm<MaterialDTO>();
  const [equation, setEquation] = useState<string>();

  return (
    <PageContainer ghost title="Доступные материалы">
      <ProCard

        style={{
          borderRadius: '2em',
          boxShadow: '0 0 1em gray',
          padding: '1em',
          marginBottom: '2em'
        }}
      >
        <ProForm<MaterialDTO>
        onFinish={(materialDTO) => handleAddMaterial(materialDTO)}
        form={form}
        onValuesChange={() => {
            returnEquation();
        }}
        submitter={{
            render(props, dom) {
                return [
                    <Button type="dashed" onClick={() => form.resetFields()}>Сбросить</Button>,
                    <Button type='primary' htmlType='submit'>  Добавить</Button>
                ]
            },
        }}>
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
          <Divider/>
          <ProForm.Group style={{ width: '100%' }} align='center'>
            <ProFormDigit style={{width: 200}} min={-10} max={10} required name="aCoeff" label="A" placeholder={''} initialValue={0}/>
            <ProFormDigit style={{width: 200}} min={-10} max={10} required name="bCoeff" label="B" placeholder={''} initialValue={0}/>
            <ProFormDigit style={{width: 200}} min={-10} max={10} required name="cCoeff" label="C" placeholder={''} initialValue={0}/>
            <Typography.Title level={5} style={{height: '100%'}}>Итоговый вид уравнения: {equation}</Typography.Title>
          </ProForm.Group>
          <Divider/>
          <ProForm.Group>
            <Flex vertical align='center' justify='space-between' style={{width: 200}}>
              <Typography.Text>Создать группу</Typography.Text>
              <Checkbox
                value={isNewGroup}
                onChange={() => setIsNewGroup((prev) => !prev)}
              />
            </Flex>
            {!isNewGroup ? (
              <ProFormSelect
              style={{width: 200}}
                label="Группа"
                name="materialGroupID"
                options={groups}
              />
            ) : (
              <ProFormText name="materialGroupName" label="Название группы" style={{width: 200}}/>
            )}
          </ProForm.Group>
        </ProForm>
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
